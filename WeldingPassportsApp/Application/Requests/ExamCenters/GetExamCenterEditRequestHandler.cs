using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    internal class GetExamCenterEditRequestHandler : IRequestHandler<GetExamCenterEditRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;

        public GetExamCenterEditRequestHandler(IExamCentersSQLRepository repository, ICompaniesSQLRepository companiesSQLRepository)
        {
            _repository=repository;
            _companiesSQLRepository=companiesSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExamCenterEditRequest request, CancellationToken cancellationToken)
        {
            if (request.EncryptedID == "null")
            {
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.returnUrl = request.ReturnUrl;

            request.Controller.ViewBag.currentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            ExamCenterEditViewModel vm = await _repository.GetExamCentersEdit(request.EncryptedID);
            //Todo: CompanySelectList unused Companies
            vm.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned:true, vm.CompanyID);
            
            return request.Controller.View(vm);
        }
    }
}
