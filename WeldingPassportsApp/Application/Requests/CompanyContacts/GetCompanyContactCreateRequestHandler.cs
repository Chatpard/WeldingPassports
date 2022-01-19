using Application.Interfaces.Repositories.SQL;
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
    public class GetCompanyContactCreateRequestHandler : IRequestHandler<GetCompanyContactCreateRequest, IActionResult>
    {
        private readonly IContactsSQLRepository _contactsSQLRepository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly IAddressesSQLRepository _addressesSQLRepository;

        public GetCompanyContactCreateRequestHandler(IContactsSQLRepository contactsSQLRepository, ICompaniesSQLRepository companiesSQLRepository, IAddressesSQLRepository addressesSQLRepository)
        {
            _contactsSQLRepository = contactsSQLRepository;
            _companiesSQLRepository = companiesSQLRepository;
            _addressesSQLRepository = addressesSQLRepository;
        }

        public async Task<IActionResult> Handle(GetCompanyContactCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            CompanyContactCreateViewModel vm = new CompanyContactCreateViewModel
            {
                ContactSelectList = _contactsSQLRepository.ContactSelectList(),
                CompanySelectList = _companiesSQLRepository.CompanySelectList(),
                AddressSelectList = _addressesSQLRepository.AddressSelectList()
            };

            return request.Controller.View(vm);
        }
    }
}
