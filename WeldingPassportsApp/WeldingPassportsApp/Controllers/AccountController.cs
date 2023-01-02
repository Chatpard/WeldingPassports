using Application.Requests.Account;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public AccountController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            try
            {
                var query = new GetAccountLoginRequest(returnUrl, this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountLogin");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            try
            {
                var query = new PostAccountExternalLoginRequest(provider, returnUrl, nameof(ExternalLoginCallback), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostAccountExternalLogin");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            try
            {
                var query = new GetAccountExternalLoginCallbackRequest(returnUrl, remoteError, nameof(Register),
                    nameof(Login), nameof(RegistrationSuccess), nameof(EmailConfirmed), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountExternalLoginCallback");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            try
            {
                var query = new GetAccountRegisterRequest(this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountRegister");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationViewModel vm)
        {
            try
            {
                var query = new PostAccountRegisterRequest(vm, nameof(ConfirmEmail), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostAccountRegister");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userID, string token, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetAccountConfirmEmailRequest(userID, token, nameof(EmailConfirmed), this);

                return await _mediator.Send(query);

            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountConfirmEmail");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmed()
        {
            try
            {
                var query = new GetAccountEmailConfirmedRequest(this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountEmailConfirmed");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrationSuccess()
        {
            try
            {
                var query = new GetAccountRegistrationSuccessRequest(this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in GetAccountRegistrationSuccess");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var query = new PostAccountLogoutRequest(nameof(PEPassportsController.Index),
                    typeof(PEPassportsController).GetNameOfController(), this);

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                return Utilities.ErrorView(_env, this, e, "Error in PostAccountLogout");
            }
        }
    }
}
