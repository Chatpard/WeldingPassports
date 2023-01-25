using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    public class GetExamCenterCreateRequestHandler : IRequestHandler<GetExamCenterCreateRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;

        public GetExamCenterCreateRequestHandler(ICompaniesSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(GetExamCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = new ExamCenterCreateViewModel();
            vm.CompanySelectList = _repository.CompanySelectList(unasigned: true);

            return request.Controller.View(vm);
        }
    }
}
