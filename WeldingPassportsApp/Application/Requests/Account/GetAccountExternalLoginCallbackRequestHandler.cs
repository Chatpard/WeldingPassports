using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountExternalLoginCallbackRequestHandler : IRequestHandler<GetAccountExternalLoginCallbackRequest, IActionResult>
    {
        private readonly IUserToApproveRepository _userToApproveRepository;

        public GetAccountExternalLoginCallbackRequestHandler(IUserToApproveRepository userToApproveRepository)
        {
            _userToApproveRepository = userToApproveRepository;
        }

        public async Task<IActionResult> Handle(GetAccountExternalLoginCallbackRequest request, CancellationToken cancellationToken)
        {
            request.ReturnUrl ??= request.Controller.Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = request.ReturnUrl,
                ExternalLogins = (await request.SignInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (request.RemoteError != null)
            {
                request.Controller.ModelState.AddModelError(string.Empty, $"Error from external provider: {request.RemoteError}");

                return request.Controller.View(request.NameOfLoginAction, loginViewModel);
            }

            var info = await request.SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                request.Controller.ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return request.Controller.View(request.NameOfLoginAction, loginViewModel);
            }

            var signInResult = await request.SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                                        isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
                return request.Controller.LocalRedirect(request.ReturnUrl);
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    AppUser user = await request.UserManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        var userToApprove = await _userToApproveRepository.GetUserToApproveByEmailAsync(email);

                        if(userToApprove == null)
                            return request.Controller.RedirectToAction(request.NameOfRegisterAction);

                        if (!userToApprove.EmailConfirmed)
                        {
                            request.Controller.HttpContext.Response.Cookies.Append(
                                "RegistrationSuccess",
                                "true",
                                new CookieOptions
                                {
                                    Expires = DateTime.Now.AddMinutes(30),
                                    IsEssential = true
                                }
                            );

                            return request.Controller.RedirectToAction(request.NameOfRegisterAction,
                                request.Controller.ControllerContext.ActionDescriptor.ControllerName);
                        }

                        request.Controller.HttpContext.Response.Cookies.Append(
                                "EmailConfirmed",
                                "true",
                                new CookieOptions
                                {
                                    Expires = DateTime.Now.AddMinutes(30),
                                    IsEssential = true
                                }
                            );

                        return request.Controller.RedirectToAction(request.NameOfEmailConfirmedAction,
                            request.Controller.ControllerContext.ActionDescriptor.ControllerName);
                    }

                    await request.UserManager.AddLoginAsync(user, info);
                    await request.SignInManager.SignInAsync(user, isPersistent: false);

                    return request.Controller.LocalRedirect(request.ReturnUrl);
                }

                request.Controller.ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                //request.Controller.ViewBag.ErrorMessage = $"Please contact support at it@synergrid.be";

                return request.Controller.View("Error");
            }
        }
    }
}
