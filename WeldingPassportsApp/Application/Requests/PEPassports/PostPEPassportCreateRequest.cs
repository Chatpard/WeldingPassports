using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.PEPassports
{
    public class PostPEPassportCreateRequest : IRequest<IActionResult>
    {
        public PostPEPassportCreateRequest(PEPassportCreateViewModel pepassport, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            PEPassportVM = pepassport;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public PEPassportCreateViewModel PEPassportVM { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
