using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterCreateRequest : IRequest<IActionResult>
    {
        public GetExamCenterCreateRequest(string returnUrl, Controller controller)
        {
            ReturnUrl=returnUrl;
            Controller=controller;
        }

        public string ReturnUrl { get; }
        public Controller Controller { get; }
    }
}
