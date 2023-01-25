using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterDeleteRequest: IRequest<IActionResult>
    {
        public GetExamCenterDeleteRequest(string encryptedID, string returnUrl, ExamCenterIndexViewModel examCenterIndexVM, Controller controller)
        {
            EncryptedID=encryptedID;
            ReturnUrl=returnUrl;
            ExamCenterIndexVM=examCenterIndexVM;
            Controller=controller;
        }

        public string EncryptedID { get; }
        public string ReturnUrl { get; }
        public ExamCenterIndexViewModel ExamCenterIndexVM { get; }
        public Controller Controller { get; }
    }
}
