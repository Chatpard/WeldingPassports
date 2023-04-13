using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    internal class GetExamCenterDetailsRequestHandler : IRequestHandler<GetExamCenterDetailsRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;

        public GetExamCenterDetailsRequestHandler(IExamCentersSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(GetExamCenterDetailsRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            ExamCenterDetailsViewModel vm = await _repository.GetExamCentersDetailsAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}