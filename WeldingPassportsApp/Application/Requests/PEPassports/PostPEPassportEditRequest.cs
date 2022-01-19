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
        public PostPEPassportEditRequest(PEPassportEditViewModel pepassportChanges, string returnUrl, Controller controller)
        {
            PEPassportChanges = pepassportChanges;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public PEPassportEditViewModel PEPassportChanges { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
