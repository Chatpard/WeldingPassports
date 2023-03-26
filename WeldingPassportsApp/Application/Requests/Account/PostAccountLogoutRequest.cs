using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class PostAccountLogoutRequest : IRequest<IActionResult>
    {
        public string NameOfIndexAction { get; }
        public string NameOfPEPassportsController { get; }
        public SignInManager<AppUser> SignInManager { get; }
        public Controller Controller { get; }

        public PostAccountLogoutRequest(string nameOfIndexAction, string nameOfPEPassportsController, SignInManager<AppUser> signInManager, Controller controller)
        {
            NameOfIndexAction = nameOfIndexAction;
            NameOfPEPassportsController = nameOfPEPassportsController;
            SignInManager = signInManager;
            Controller = controller;
        }
    }
}
