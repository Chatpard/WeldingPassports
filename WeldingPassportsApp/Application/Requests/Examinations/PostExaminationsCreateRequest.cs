using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class PostExaminationsCreateRequest : IRequest<IActionResult>
    {
        public ExaminationCreateViewModel Vm { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostExaminationsCreateRequest(ExaminationCreateViewModel vm, string returnUrl, Controller controller)
        {
            Vm = vm;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
