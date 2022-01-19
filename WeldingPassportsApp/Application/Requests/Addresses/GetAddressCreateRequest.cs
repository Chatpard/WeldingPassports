using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Addresses
{
    public class GetAddressCreateRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetAddressCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }

    }
}
