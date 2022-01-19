using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Account
{
    public class GetAccountRegisterRequest : IRequest<IActionResult>
    {
        public Controller Controller { get; }

        public GetAccountRegisterRequest(Controller controller)
        {
            Controller = controller;
        }
    }
}
