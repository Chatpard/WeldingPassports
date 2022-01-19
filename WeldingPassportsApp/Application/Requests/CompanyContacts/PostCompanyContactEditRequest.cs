using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyContactEditRequest : IRequest<IActionResult>
    {
        public CompanyContactEditViewModel ContactChanges { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostCompanyContactEditRequest(CompanyContactEditViewModel contactChanges, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            ContactChanges = contactChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
