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
        public CompanyContactEditViewModel ContactChanges { get; }
        public string NameOfDetailsAction { get; }
        public string ReturnUrl { get; }
        public string CurrentUrl { get; }
        public Controller Controller { get; }

        public PostCompanyContactEditRequest(CompanyContactEditViewModel contactChanges, string nameOfDetailsAction, string returnUrl, string currentUrl, Controller controller)
        {
            ContactChanges = contactChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            CurrentUrl=currentUrl;
            Controller = controller;
        }
    }
}
