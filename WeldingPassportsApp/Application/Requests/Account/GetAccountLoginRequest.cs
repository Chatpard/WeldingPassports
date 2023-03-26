using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Account
{
    public class GetAccountLoginRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public Controller Controller { get; }

        public GetAccountLoginRequest(string returnUrl, SignInManager<AppUser> signInManager, Controller controller)
        {
            ReturnUrl = returnUrl;
            SignInManager = signInManager;
            Controller = controller;
        }
    }
}
