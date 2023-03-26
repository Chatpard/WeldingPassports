using Domain.Models;
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
    public class GetAccountRegisterRequest : IRequest<IActionResult>
    {
        public SignInManager<AppUser> SignInManager { get; }
        public Controller Controller { get; }

        public GetAccountRegisterRequest(SignInManager<AppUser> signInManager, Controller controller)
        {
            SignInManager = signInManager;
            Controller = controller;
        }
    }
}
