using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Certificates
{
    public class PostCertificatesCreateRequest : IRequest<IActionResult>
    {
        public string ExaminationEncryptedID { get; }
        public CertificateCreateViewModel VM { get; }
        public string ReturnUrl { get; set; }
        public Controller Controller { get; }

        public PostCertificatesCreateRequest(string examinationEncryptedID, CertificateCreateViewModel vm, string returnUrl, Controller controller)
        {
            ExaminationEncryptedID = examinationEncryptedID;
            VM = vm;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
