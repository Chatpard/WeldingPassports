using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class GetAccountExternalLoginCallbackRequest : IRequest<IActionResult>
    {
        public string ReturnUrl { get; set; }
        public string RemoteError { get; }
        public string NameOfRegisterAction { get; }
        public string NameOfLoginAction { get; }
        public string NameOfRegistrationSuccessAction { get; }
        public string NameOfEmailConfirmedAction { get; }
        public Controller Controller { get; }

        public GetAccountExternalLoginCallbackRequest(string returnUrl, string remoteError, string nameOfRegisterAction,
            string nameOfLoginAction, string nameOfRegistrationSuccessAction, string nameOfEmailConfirmedAction,
            Controller controller)
        {
            ReturnUrl = returnUrl;
            RemoteError = remoteError;
            NameOfRegisterAction = nameOfRegisterAction;
            NameOfLoginAction = nameOfLoginAction;
            NameOfRegistrationSuccessAction = nameOfRegistrationSuccessAction;
            NameOfEmailConfirmedAction = nameOfEmailConfirmedAction;
            Controller = controller;
        }
    }
}
