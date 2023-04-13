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

namespace Application.Requests.TrainingCenters
{
    class GetTrainingCenterCreateRequestHandler : IRequestHandler<GetTrainingCenterCreateRequest, ActionResult>
    {
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly ICompanyContactsSQLRepository _companyContactsSQLRepository;

        public GetTrainingCenterCreateRequestHandler(ICompaniesSQLRepository companiesSQLRepository, ICompanyContactsSQLRepository companyContactsSQLRepository)
        {
            _companiesSQLRepository = companiesSQLRepository;
            _companyContactsSQLRepository = companyContactsSQLRepository;
        }

        public async Task<ActionResult> Handle(GetTrainingCenterCreateRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            TrainingCenterCreateViewModel vm = new TrainingCenterCreateViewModel
            {
                CompanySelectList = _companiesSQLRepository.CompanySelectList(unasigned: true),
                CompanyContactSelectList = _companyContactsSQLRepository.CompanyContactTrainingCenterSelectList(),
                IsActive = true
            };

            return request.Controller.View(vm);
        }
    }
}
