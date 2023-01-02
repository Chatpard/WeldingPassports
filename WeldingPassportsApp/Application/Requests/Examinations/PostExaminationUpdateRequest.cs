using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class PostExaminationUpdateRequest : IRequest<IActionResult>
    {
        public ExaminationUpdateViewModel ExaminationUpdates { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostExaminationUpdateRequest(ExaminationUpdateViewModel examinationUpdates, string returnUrl, Controller controller)
        {
            ExaminationUpdates = examinationUpdates;
            ReturnUrl = returnUrl;
            Controller = controller;
        }

    }
}
