using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Examinations
{
    public class PostExaminationsCreateRequest : IRequest<IActionResult>
    {
        public ExaminationCreateViewModel Vm { get; }
        public string ReturnUrl { get; }
        public string ExaminationsControllerName { get; }
        public string ExaminationEditActionName { get; }
        public Controller Controller { get; }

        public PostExaminationsCreateRequest(ExaminationCreateViewModel vm, string returnUrl, string examinationsControllerName, string examinationEditActionName, Controller controller)
        {
            Vm = vm;
            ReturnUrl = returnUrl;
            ExaminationsControllerName = examinationsControllerName;
            ExaminationEditActionName = examinationEditActionName;
            Controller = controller;
        }
    }
}
