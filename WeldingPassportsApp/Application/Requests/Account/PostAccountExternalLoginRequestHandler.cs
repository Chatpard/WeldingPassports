using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class PostAccountExternalLoginRequestHandler : IRequestHandler<PostAccountExternalLoginRequest, ChallengeResult>
    {
        public PostAccountExternalLoginRequestHandler()
        {

        }

        public Task<ChallengeResult> Handle(PostAccountExternalLoginRequest request, CancellationToken cancellationToken)
        {
            var controller = request.Controller;

            var redirectUrl = controller.Url.Action(request.NameOfExternalLoginCallbackAction,
                controller.ControllerContext.ActionDescriptor.ControllerName, new { request.ReturnUrl });

            var properties = request.SignInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl);

            var challengeResult = new ChallengeResult(request.Provider, properties);

            return Task.FromResult(challengeResult);
        }
    }
}
