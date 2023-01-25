using Application.Interfaces.Repositories.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.ExamCentersAPI
{
    public class IsCompanyIDInUseRequestHandler: IRequestHandler<IsCompanyIDInUseRequest, JsonResult>
    {
        private readonly IExamCentersAPIRepository _repository;

        public IsCompanyIDInUseRequestHandler(IExamCentersAPIRepository repository)
        {
            _repository=repository;
        }

        public async Task<JsonResult> Handle(IsCompanyIDInUseRequest request, CancellationToken cancellationToken)
        {
            return new JsonResult(_repository.IsCompanyIDInUse(request.CompanyID));
        }
    }
}
