using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Addresses
{
    public class PostAddressEditRequest : IRequest<IActionResult>
    {
        public PostAddressEditRequest(AddressEditViewModel addressChanges, string nameOfDetailsAction, string returnUrl, Controller controller)
        {
            AddressChanges=addressChanges;
            NameOfDetailsAction=nameOfDetailsAction;
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public AddressEditViewModel AddressChanges { get; }
        public string NameOfDetailsAction { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
