using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyContactCreateRequest : IRequest<IActionResult>
    {
        public CompanyContactCreateViewModel ContactVM { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostCompanyContactCreateRequest(CompanyContactCreateViewModel contact, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            ContactVM = contact;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
