using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Certificates
{
    public class PostCertificatesEditRequest : IRequest<IActionResult>
    {
        public CertificateEditViewModel VM { get; set; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostCertificatesEditRequest(CertificateEditViewModel vm, string returnUrl, Controller controller)
        {
            VM = vm;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
