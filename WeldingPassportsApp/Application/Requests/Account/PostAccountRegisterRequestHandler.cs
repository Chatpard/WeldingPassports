using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class PostAccountRegisterRequestHandler : IRequestHandler<PostAccountRegisterRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _repository;
        private readonly IMailService _mailService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PostAccountRegisterRequestHandler> _localizer;

        public PostAccountRegisterRequestHandler(IUserToApproveRepository repository, IMailService mailService,
            IConfiguration config, IMapper mapper, IStringLocalizer<PostAccountRegisterRequestHandler> localizer)
        {
            _repository = repository;
            _mailService = mailService;
            _config = config;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<IActionResult> Handle(PostAccountRegisterRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                var userToApprove = _mapper.Map<UserToApprove>(request.ViewModel);
                userToApprove.EmailLanguage = request.Controller.Request.HttpContext.Features
                    .Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

                var userToApproveEntityEntry = _repository.InsertUserToApprove(userToApprove);
                await _repository.SaveChangesAsync(cancellationToken);
                
                userToApprove = _repository.EncryptUserToApproveID(userToApproveEntityEntry.Entity);

                var token = JwtValidation.GenerateToken(userToApproveEntityEntry.Entity.ID, _config);

                var confirmationLink = request.Controller.Url.Action(request.NameOfConfirmEmailAction,
                    request.Controller.ControllerContext.ActionDescriptor.ControllerName,
                    new { userID = userToApprove.EncryptedId, token = token }, request.Controller.Request.Scheme);

                // Send email
                var content = "<h1>" + _localizer["Your registration is almost done!"] +
                    "</h1><p>" + _localizer["Please click on following link to complete your registration:"] +
                    " <a href=\"" + confirmationLink + "\">" + _localizer["link"] + "</a></p>";
                await _mailService.SendEmailAsync(request.ViewModel.Email, _localizer["Account Confirmation"], content);

                request.Controller.HttpContext.Response.Cookies.Append(
                    "RegistrationSuccess",
                    "true",
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        IsEssential = true,
                    }
                );

                return request.Controller.RedirectToAction("RegistrationSuccess", "Account");
            }

            return request.Controller.View(request.ViewModel);
        }
    }
}
