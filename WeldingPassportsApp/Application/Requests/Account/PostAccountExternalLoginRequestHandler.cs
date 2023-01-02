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
        private readonly SignInManager<IdentityUser> _signInManager;

        public PostAccountExternalLoginRequestHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<ChallengeResult> Handle(PostAccountExternalLoginRequest request, CancellationToken cancellationToken)
        {
            var controller = request.Controller;

            var redirectUrl = controller.Url.Action(request.NameOfExternalLoginCallbackAction,
                controller.ControllerContext.ActionDescriptor.ControllerName, new { request.ReturnUrl });

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, redirectUrl);

            return Task.FromResult(new ChallengeResult(request.Provider, properties));
        }
    }
}
