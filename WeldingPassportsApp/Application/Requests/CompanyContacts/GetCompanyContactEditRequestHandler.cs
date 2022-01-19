using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class GetCompanyContactEditRequestHandler : IRequestHandler<GetCompanyContactEditRequest, IActionResult>
    {
        private readonly ICompanyContactsSQLRepository _repository;
        private readonly IContactsSQLRepository _contactsSQLRepository;
        private readonly ICompaniesSQLRepository _companiesSQLRepository;
        private readonly IAddressesSQLRepository _addressesSQLRepository;

        public GetCompanyContactEditRequestHandler(ICompanyContactsSQLRepository repository, IContactsSQLRepository contactsSQLRepository, ICompaniesSQLRepository companiesSQLRepository, IAddressesSQLRepository addressesSQLRepository)
        {
            _repository = repository;
            _contactsSQLRepository = contactsSQLRepository;
            _companiesSQLRepository = companiesSQLRepository;
            _addressesSQLRepository = addressesSQLRepository;
        }

        public async Task<IActionResult> Handle(GetCompanyContactEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetCompanyContactEditAsync(request.EncryptedID);
            vm.ContactSelectList = _contactsSQLRepository.ContactSelectList();
            vm.CompanySelectList = _companiesSQLRepository.CompanySelectList();
            vm.AddressSelectList = _addressesSQLRepository.AddressSelectList();

            return request.Controller.View(vm);
        }
    }
}
