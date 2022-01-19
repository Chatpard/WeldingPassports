using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Welders
{
    public class PostPEWelderEditRequest : IRequest<IActionResult>
    {
        public PEWelderEditViewModel PEWelderChanges { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostPEWelderEditRequest(PEWelderEditViewModel peWelderChanges, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            PEWelderChanges = peWelderChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
