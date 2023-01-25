using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterDetailsRequest: IRequest<IActionResult>
    {
        public GetExamCenterDetailsRequest(string encryptedID, string returnUrl, Controller controller)
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
