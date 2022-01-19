using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Welders
{
    public class PostPEWelderCreateRequest : IRequest<IActionResult>
    {
        public PEWelderCreateViewModel PEWelderVM { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostPEWelderCreateRequest(PEWelderCreateViewModel peWelder, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            PEWelderVM = peWelder;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
