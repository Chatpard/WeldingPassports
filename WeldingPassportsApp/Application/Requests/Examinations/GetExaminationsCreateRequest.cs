using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class GetExaminationsCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }

        public GetExaminationsCreateRequest(string returnUrl, UserManager<AppUser> userManager, Controller controller)
        {
            ReturnUrl = returnUrl;
            UserManager = userManager;
            Controller = controller;
        }
    }
}
