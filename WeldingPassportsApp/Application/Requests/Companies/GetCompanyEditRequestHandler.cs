using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Companies
{
    public class GetCompanyContactEditRequestHandler : IRequestHandler<GetCompanyEditRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;
        private readonly IAddressesSQLRepository _addressesSQLRepository;

        public GetCompanyContactEditRequestHandler(ICompaniesSQLRepository repository, IAddressesSQLRepository addressesSQLRepository)
        {
            _repository = repository;
            _addressesSQLRepository = addressesSQLRepository;
        }

        public async Task<IActionResult> Handle(GetCompanyEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetCompanyEditAsync(request.EncryptedID);
            vm.AddressSelectList = _addressesSQLRepository.AddressSelectList();

            return request.Controller.View(vm);
        }
    }
}
