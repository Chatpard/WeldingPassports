﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class GetExaminationsCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public GetExaminationsCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
