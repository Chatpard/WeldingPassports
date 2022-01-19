using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class GetAccountEmailConfirmedRequest : IRequest<IActionResult>
    {
        public Controller Controller { get; }

        public GetAccountEmailConfirmedRequest(Controller controller)
        {
            Controller = controller;
        }
    }
}
