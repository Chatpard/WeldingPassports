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

namespace Application.Requests.PEPassports
{
    public class GetPEPassportCreateRequestHandler : IRequestHandler<GetPEPassportCreateRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _pePassportsSQLRepository;
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;


        public GetPEPassportCreateRequestHandler(IPEPassportsSQLRepository pePassportsSQLRepository, IPEWeldersSQLRepository peWeldersSQLRepository, ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _pePassportsSQLRepository = pePassportsSQLRepository;
            _peWeldersSQLRepository = peWeldersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetPEPassportCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = new PEPassportCreateViewModel
            {
                TrainingCenterSelectList = _trainingCentersSQLRepository.TrainingCenterSelectList(),
                PEWelderSelectList = _peWeldersSQLRepository.PEWelderSelectList()
            };


            return request.Controller.View(vm);
        }
    }
}
