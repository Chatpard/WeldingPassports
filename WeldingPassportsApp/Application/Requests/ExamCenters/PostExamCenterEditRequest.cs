using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.ExamCenters
{
    public class PostExamCenterEditRequest: IRequest<IActionResult>
    {
        public PostExamCenterEditRequest(ExamCenterEditViewModel examCentersEditVM, string returnUrl, Controller controller)
        {
            ExamCenterEditVM=examCentersEditVM;
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public ExamCenterEditViewModel ExamCenterEditVM { get; }
        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
