using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportEditRequestHandler : IRequestHandler<GetPEPassportEditRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _repository;
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;

        public GetPEPassportEditRequestHandler(IPEPassportsSQLRepository repository, IPEWeldersSQLRepository peWeldersSQLRepository, ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _repository = repository;
            _peWeldersSQLRepository = peWeldersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetPEPassportEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetPEPassportEditAsync(request.EncryptedID);
            vm.TrainingCenterSelectList = _trainingCentersSQLRepository.TrainingCenterSelectList();
            vm.PEWelderSelectList = _peWeldersSQLRepository.PEWelderSelectList();

            return request.Controller.View(vm);
        }
    }
}
