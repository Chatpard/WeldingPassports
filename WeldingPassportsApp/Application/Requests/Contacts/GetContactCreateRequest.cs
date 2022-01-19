using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Contacts
{
    public class GetContactCreateRequest : IRequest<IActionResult>
    {
        public GetContactCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public string ReturnUrl { get; }

        public Controller Controller { get; }
    }
}
