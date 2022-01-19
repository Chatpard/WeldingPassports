using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Certificates
{
    public class GetCertificatesCreateRequest : IRequest<IActionResult>
    {
        public string ExaminationEncryptedID { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public GetCertificatesCreateRequest(string examinationEncryptedID, string returnUrl, Controller controller)
        {
            ExaminationEncryptedID = examinationEncryptedID;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
