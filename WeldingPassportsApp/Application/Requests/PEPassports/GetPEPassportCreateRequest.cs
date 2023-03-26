﻿using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public UserManager<AppUser> UserManager { get; }
        public Controller Controller { get; }

        public GetPEPassportCreateRequest(string returnUrl, UserManager<AppUser> userManager, Controller controller)
        {
            ReturnUrl = returnUrl;
            UserManager = userManager;
            Controller = controller;
        }
    }
}
