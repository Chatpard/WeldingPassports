using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class GetAdminDeleteUserRequest : IRequest<IActionResult>
    {
        public string UserToApproveEncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetAdminDeleteUserRequest(string userToApproveEncryptedID, string returnUrl, Controller controller)
        {
            UserToApproveEncryptedID = userToApproveEncryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
