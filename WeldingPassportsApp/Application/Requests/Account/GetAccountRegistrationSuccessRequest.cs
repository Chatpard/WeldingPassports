using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class GetAccountRegistrationSuccessRequest : IRequest<IActionResult>
    {
        public Controller Controller { get; }

        public GetAccountRegistrationSuccessRequest(Controller controller)
        {
            Controller = controller;
        }
    }
}
