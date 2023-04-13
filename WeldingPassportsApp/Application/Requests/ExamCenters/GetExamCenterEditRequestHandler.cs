using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    internal class GetExamCenterEditRequestHandler : IRequestHandler<GetExamCenterEditRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;

        public GetExamCenterEditRequestHandler(
            IExamCentersSQLRepository repository, 
            ICompaniesSQLRepository companiesSQLRepository,
            ICompanyContactsSQLRepository companyContactsSQLRepository)
        {
            _repository=repository;
            _companiesSQLRepository=companiesSQLRepository;
            _companyContactsSQLRepository=companyContactsSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExamCenterEditRequest request, CancellationToken cancellationToken)
        {
            if (request.EncryptedID == "null")
            {
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            ExamCenterEditViewModel vm = await _repository.GetExamCentersEdit(request.EncryptedID);
            //Todo: CompanySelectList unused Companies
            vm.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned:true, CompanyID:vm.CompanyID);
            vm.CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactExamCenterSelectList(vm.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
