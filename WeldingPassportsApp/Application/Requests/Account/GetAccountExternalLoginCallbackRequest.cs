using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Account
{
    public class GetAccountExternalLoginCallbackRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public string RemoteError { get; }
        public string NameOfRegisterAction { get; }
        public string NameOfLoginAction { get; }
        public string NameOfRegistrationSuccessAction { get; }
        public string NameOfEmailConfirmedAction { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }

        public GetAccountExternalLoginCallbackRequest(string returnUrl, string remoteError, string nameOfRegisterAction,
            string nameOfLoginAction, string nameOfRegistrationSuccessAction, string nameOfEmailConfirmedAction, 
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, Controller controller)
        {
            ReturnUrl = returnUrl;
            RemoteError = remoteError;
            NameOfRegisterAction = nameOfRegisterAction;
            NameOfLoginAction = nameOfLoginAction;
            NameOfRegistrationSuccessAction = nameOfRegistrationSuccessAction;
            NameOfEmailConfirmedAction = nameOfEmailConfirmedAction;
            SignInManager = signInManager;
            UserManager = userManager;
            Controller = controller;
        }
    }
}
