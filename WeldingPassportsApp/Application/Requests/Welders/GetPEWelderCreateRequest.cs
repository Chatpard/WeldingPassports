using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Welders
{
    public class GetPEWelderCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public GetPEWelderCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
