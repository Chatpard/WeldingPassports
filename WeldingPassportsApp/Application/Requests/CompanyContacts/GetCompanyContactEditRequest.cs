﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.CompanyContacts
{
    public class GetCompanyContactEditRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetCompanyContactEditRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
