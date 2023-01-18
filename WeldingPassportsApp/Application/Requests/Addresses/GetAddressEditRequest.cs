using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Addresses
{
    public class GetAddressEditRequest : IRequest<IActionResult>
    {
        public GetAddressEditRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID=encryptedID;
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
