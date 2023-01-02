using Application.Interfaces.Repositories.SQL;
using Application.Requests.TrainingCenters;
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
    public class GetTrainingCenterDetailsRequestHandler : IRequestHandler<GetTrainingCenterDetailsRequest, IActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;

        public GetTrainingCenterDetailsRequestHandler(ITrainingCentersSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetTrainingCenterDetailsRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetTrainingCenterDetailsAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
