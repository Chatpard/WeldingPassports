using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Examinations
{
    public class PostExaminationEditRequest : IRequest<IActionResult>
    {
        public ExaminationEditViewModel NewExaminationVm { get; }
        public string NameOfDetailsAction { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }

        public PostExaminationEditRequest(ExaminationEditViewModel newExaminationVm, string nameOfDetailsAction,
            string returnUrl, Controller controller)
        {
            NewExaminationVm = newExaminationVm;
            NameOfDetailsAction = nameOfDetailsAction;
            ReturnUrl = returnUrl;
            Controller = controller;
        }
    }
}
