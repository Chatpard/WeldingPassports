using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Admin
{
    public class PostAdminApproveUserRequest : IRequest<IActionResult>
    {
        public string UserToApproveEncryptedID { get; }
        public string Role { get; }
        public string NameOfUsersToApproveIndexAction { get; }
        public string NameOfLoginAction { get; }
        public string NameOfAccountController { get; }
        public Controller Controller { get; }

        public PostAdminApproveUserRequest(string userToApproveEncryptedID, string role, string nameOfUsersToApproveIndexAction,
            string nameOfLoginAction, string nameOfAccountController, Controller controller)
        {
            UserToApproveEncryptedID = userToApproveEncryptedID;
            Role = role;
            NameOfUsersToApproveIndexAction = nameOfUsersToApproveIndexAction;
            NameOfLoginAction = nameOfLoginAction;
            NameOfAccountController = nameOfAccountController;
            Controller = controller;
        }
    }
}
