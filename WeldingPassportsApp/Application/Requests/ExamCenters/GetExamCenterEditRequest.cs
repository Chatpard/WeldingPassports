using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterEditRequest: IRequest<IActionResult>
    {
        public GetExamCenterEditRequest(string encryptedID, string returnUrl, Controller controller)
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
