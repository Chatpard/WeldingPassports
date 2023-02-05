using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Admin
{
    public class PostAdminApproveUserRequestHandler : IRequestHandler<PostAdminApproveUserRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _repository;
        private readonly IMailService _mailService;
        private readonly IStringLocalizer<PostAdminApproveUserRequestHandler> _localizer;

        public PostAdminApproveUserRequestHandler(IUserToApproveRepository repository, IMailService mailService,
            IStringLocalizer<PostAdminApproveUserRequestHandler> localizer)
        {
            _repository = repository;
            _mailService = mailService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Handle(PostAdminApproveUserRequest request, CancellationToken cancellationToken)
        {
            var language = await _repository.GetUserToApproveEmailLanguageByEncryptedIDAsync(request.UserToApproveEncryptedID);

            IdentityUser user = await _repository
                .InsertAppUserByEncryptedIDAsync(request.UserToApproveEncryptedID, request.Role, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            var loginLink = request.Controller.Url.Action(request.NameOfLoginAction, request.NameOfAccountController, null,
            request.Controller.Request.Scheme);

            // Send email
            var currentUICulture = CultureInfo.CurrentUICulture;
            CultureInfo.CurrentUICulture = new CultureInfo(language);
            var content = "<h1>" + _localizer["Your account has been approved!"] + "</h1><p>" +
                _localizer["Synergrid has reviewed and approved your account. You can login here:"] +
                " <a href=\"" + loginLink + "\">" + _localizer["link"] + "</a></p>";
            await _mailService.SendEmailAsync(user.Email, _localizer["Account Approval"], content);
            CultureInfo.CurrentUICulture = currentUICulture;

            return request.Controller.RedirectToAction(request.NameOfUsersToApproveIndexAction);
        }
    }
}
