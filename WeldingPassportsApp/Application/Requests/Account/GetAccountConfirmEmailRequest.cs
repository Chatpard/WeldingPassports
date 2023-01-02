using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Requests.Account
{
    public class GetAccountConfirmEmailRequest : IRequest<IActionResult>
    {
        public string EncryptedUserID { get; }
        public string Token { get; }
        public string NameOfEmailConfirmedAction { get; }
        public Controller Controller { get; }

        public GetAccountConfirmEmailRequest(string encryptedUserID, string token,
            string nameOfEmailConfirmedAction, Controller controller)
        {
            EncryptedUserID = encryptedUserID;
            Token = token;
            NameOfEmailConfirmedAction = nameOfEmailConfirmedAction;
            Controller = controller;
        }
    }
}
