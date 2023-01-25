using System.Threading.Tasks;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Application.Interfaces.Repositories.SQL;
using Microsoft.AspNetCore.Http.Extensions;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCenterEditRequestHandler : IRequestHandler<GetTrainingCenterEditRequest, ActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;

        public GetTrainingCenterEditRequestHandler(ITrainingCentersSQLRepository repository, ICompaniesSQLRepository companiesSQLRepository, ICompanyContactsSQLRepository companyContactsSQLRepository)
        {
            _repository = repository;
            _companiesSQLRepository = companiesSQLRepository;
            _companyContactsSQLRepository = companyContactsSQLRepository;
        }

        public async Task<ActionResult> Handle(GetTrainingCenterEditRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            TrainingCenterEditViewModel vm = await _repository.GetTrainingCenterEditAsync(request.EncryptedID);
            //Todo: CompanySelectList unused Companies
            vm.CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned:true, CompanyID:vm.CompanyID);
            vm.CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactSelectList(vm.EncryptedID, vm.CompanyContactID);

            return request.Controller.View(vm);
        }
    }
}
