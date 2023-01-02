using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Account
{
    public class PostAccountLogoutRequest : IRequest<IActionResult>
    {
        public string NameOfIndexAction { get; }
        public string NameOfPEPassportsController { get; }
        public Controller Controller { get; }

        public PostAccountLogoutRequest(string nameOfIndexAction, string nameOfPEPassportsController, Controller controller)
        {
            NameOfIndexAction = nameOfIndexAction;
            NameOfPEPassportsController = nameOfPEPassportsController;
            Controller = controller;
        }
    }
}
