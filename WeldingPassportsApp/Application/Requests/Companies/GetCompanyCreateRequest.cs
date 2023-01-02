using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Companies
{
    public class GetCompanyCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public GetCompanyCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
