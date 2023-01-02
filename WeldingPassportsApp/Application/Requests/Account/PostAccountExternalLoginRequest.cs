using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class PostAccountExternalLoginRequest : IRequest<ChallengeResult>
    {
        public string Provider { get; }
        public string ReturnUrl { get; }
        public string NameOfExternalLoginCallbackAction { get; }
        public Controller Controller { get; }

        public PostAccountExternalLoginRequest(string provider, string returnUrl, string nameOfExternalLoginCallbackAction, Controller controller)
        {
            Provider = provider;
            ReturnUrl = returnUrl;
            NameOfExternalLoginCallbackAction = nameOfExternalLoginCallbackAction;
            Controller = controller;
        }
    }
}
