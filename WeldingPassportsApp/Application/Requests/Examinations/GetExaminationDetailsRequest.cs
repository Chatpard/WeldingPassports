using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class GetExaminationDetailsRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetExaminationDetailsRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
