using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.PEPassports
{
    public class PostPEPassportEditRequest : IRequest<IActionResult>
    {
        public PEPassportEditViewModel PEPassportChanges { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostPEPassportEditRequest(PEPassportEditViewModel pepassportChanges, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            PEPassportChanges = pepassportChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
