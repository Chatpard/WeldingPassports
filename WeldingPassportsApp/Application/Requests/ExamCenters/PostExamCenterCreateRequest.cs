using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.ExamCenters
{
    public class PostExamCenterCreateRequest: IRequest<IActionResult>
    {
        public PostExamCenterCreateRequest(ExamCenterCreateViewModel examCenterCreateVM, string returnUrl,  Controller controller)
        {
            ExamCenterCreateVM=examCenterCreateVM;
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public ExamCenterCreateViewModel ExamCenterCreateVM { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
