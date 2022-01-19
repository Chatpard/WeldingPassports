using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Addresses
{
    public class PostAddressCreateRequest : IRequest<IActionResult>
    {
        public AddressCreateViewModel AddressCreateVM { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostAddressCreateRequest(AddressCreateViewModel addressCreateVM, string returnUrl, Controller controller)
        {
            AddressCreateVM = addressCreateVM;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

    }
}
