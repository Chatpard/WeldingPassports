using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountLoginRequestHandler : IRequestHandler<GetAccountLoginRequest, IActionResult>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public GetAccountLoginRequestHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(GetAccountLoginRequest request, CancellationToken cancellationToken)
        {
            var vm = new LoginViewModel
            {
                ReturnUrl = request.ReturnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return request.Controller.View(vm);
        }
    }
}
