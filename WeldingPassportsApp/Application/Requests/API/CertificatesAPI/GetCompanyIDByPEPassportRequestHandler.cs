using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    internal class GetCompanyIDByPEPassportRequestHandler : IRequestHandler<GetCompanyIDByPEPassportRequest, ActionResult<int?>>
    {
        private readonly ICertificationsAPIRepository _repository;

        public GetCompanyIDByPEPassportRequestHandler(ICertificationsAPIRepository repository)
        {
            _repository=repository;
        }

        public async Task<ActionResult<int?>> Handle(GetCompanyIDByPEPassportRequest request, CancellationToken cancellationToken)
        {
            int? companyID = await _repository.GetCompanyIDByPEPassport(request.PePassportID);
            if (companyID == null)
            {
                return request.Controller.NotFound(companyID);
            }
            return request.Controller.Ok(companyID);
        }
    }
}
