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
    public class GetCompanyDetailsRequestHandler : IRequestHandler<GetCompanyDetailsRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;

        public GetCompanyDetailsRequestHandler(ICompaniesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetCompanyDetailsAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
