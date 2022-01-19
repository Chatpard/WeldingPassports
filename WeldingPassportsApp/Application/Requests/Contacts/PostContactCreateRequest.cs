using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Contacts
{
    public class PostContactCreateRequest : IRequest<IActionResult>
    {
        public PostContactCreateRequest(ContactCreateViewModel contactCreateVM, string returnUrl, Controller controller)
        {
            ContactCreateVM = contactCreateVM;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

        public ContactCreateViewModel ContactCreateVM { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
