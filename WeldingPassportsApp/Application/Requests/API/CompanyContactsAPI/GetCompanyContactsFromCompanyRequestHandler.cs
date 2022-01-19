using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CompanyContactsAPI
{
    class GetCompanyContactsFromCompanyRequestHandler : IRequestHandler<GetCompanyContactsFromCompanyRequest, ActionResult<SelectList>>
    {
        private readonly ICompanyContactsAPIRepository _repository;

        public GetCompanyContactsFromCompanyRequestHandler(ICompanyContactsAPIRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<SelectList>> Handle(GetCompanyContactsFromCompanyRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetCompanyContactsFromCompanySelectList(request.CompanyID);
        }
    }
}
