using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class GetAdminUsersToApproveIndexRequest : IRequest<IActionResult>
    {
        public Controller Controller { get; }

        public GetAdminUsersToApproveIndexRequest(Controller controller)
        {
            Controller = controller;
        }
    }
}
