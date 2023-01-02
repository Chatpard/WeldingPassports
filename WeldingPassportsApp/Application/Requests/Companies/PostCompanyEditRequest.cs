using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Companies
{
    public class PostCompanyEditRequest : IRequest<IActionResult>
    {
        public CompanyEditViewModel CompanyChanges { get; set; }
        public string NameOfDetailsAction { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostCompanyEditRequest(CompanyEditViewModel companyChanges, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            CompanyChanges = companyChanges;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
