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
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;

        public GetExamCenterCreateRequestHandler(ICompaniesSQLRepository companiesSQLRepository, ICompanyContactsSQLRepository companyContactsSQLRepository)
        {
            _companiesSQLRepository=companiesSQLRepository;
            _companyContactsSQLRepository=companyContactsSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExamCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            ExamCenterCreateViewModel vm = new ExamCenterCreateViewModel()
            {
                CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned: true),
                CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactExamCenterSelectList(),
                IsActive = true
            };

            return request.Controller.View(vm);
        }
    }
}
