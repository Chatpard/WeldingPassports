using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class GetExaminationUpdateRequest : IRequest<IActionResult>
    {
        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetExaminationUpdateRequest(string encryptedID, string returnUrl, Controller controller)
        {
            EncryptedID = encryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

    }
}
