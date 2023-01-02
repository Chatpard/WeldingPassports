using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class GetAccountLoginRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetAccountLoginRequest(string returnUrl, Controller controller)
        {
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
