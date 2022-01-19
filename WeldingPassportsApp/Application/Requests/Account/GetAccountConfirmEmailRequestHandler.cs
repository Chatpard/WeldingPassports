using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountConfirmEmailRequestHandler : IRequestHandler<GetAccountConfirmEmailRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _repository;
        private readonly IConfiguration _config;

        public GetAccountConfirmEmailRequestHandler(IUserToApproveRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<IActionResult> Handle(GetAccountConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var controller = request.Controller;

            if (request.Token == null)
                return controller.View("NotFound");

            var user = await _repository.GetUserToApproveByEncryptedIDAsync(request.EncryptedUserID);

            if (user == null)
            {
                controller.ViewBag.ErrorMessage = $"The user ID {request.EncryptedUserID} is invalid";

                return controller.View("NotFound");
            }

            if (JwtValidation.ValidateCurrentToken(request.Token, _config))
            {
                user.EmailConfirmed = true;

                _repository.UpdateUserToApprove(user);

                await _repository.SaveAsync(cancellationToken);

                controller.HttpContext.Response.Cookies.Append(
                    "EmailConfirmed",
                    "true",
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        IsEssential = true
                    }
                );

                return controller.RedirectToAction(request.NameOfEmailConfirmedAction,
                    controller.ControllerContext.ActionDescriptor.ControllerName);
            }

            controller.ViewBag.ErrorTitle = "Email cannot be confirmed";
            return controller.View("Error");
        }
    }
}
