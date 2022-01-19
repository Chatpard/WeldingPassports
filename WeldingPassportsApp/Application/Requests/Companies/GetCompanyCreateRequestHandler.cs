using Application.Interfaces.Repositories.SQL;
using Application.Requests.Companies;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class GetCompanyCreateRequestHandler : IRequestHandler<GetCompanyCreateRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;
        private readonly IAddressesSQLRepository _addressesSQLRepository;

        public GetCompanyCreateRequestHandler(ICompaniesSQLRepository repository, IAddressesSQLRepository addressesSQLRepository)
        {
            _repository = repository;
            _addressesSQLRepository = addressesSQLRepository;
        }

        public async Task<IActionResult> Handle(GetCompanyCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            CompanyCreateViewModel vm = new CompanyCreateViewModel
            {
                AddressSelectList = _addressesSQLRepository.AddressSelectList()
            };

            return request.Controller.View(vm);
        }
    }
}
