using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyCreateRequest : IRequest<IActionResult>
    {
        public CompanyCreateViewModel ContactCreateVM { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostCompanyCreateRequest(CompanyCreateViewModel contactCreateVM, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            ContactCreateVM = contactCreateVM;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
