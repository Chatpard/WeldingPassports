using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class GetAdminUserToApproveDetailsRequest : IRequest<IActionResult>
    {
        public string Id { get; }
        public string NameOfUsersToApproveIndexAction { get; }
        public Controller Controller { get; }

        public GetAdminUserToApproveDetailsRequest(string id, string nameOfUsersToApproveIndexAction, Controller controller)
        {
            Id = id;
            NameOfUsersToApproveIndexAction = nameOfUsersToApproveIndexAction;
            Controller = controller;
        }
    }
}
