﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Welders
{
    public class GetPEWelderEditRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; set; }
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public GetPEWelderEditRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
