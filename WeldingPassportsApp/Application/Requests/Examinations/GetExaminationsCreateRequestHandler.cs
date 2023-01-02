using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class GetExaminationsCreateRequestHandler 
        : IRequestHandler<GetExaminationsCreateRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _examCentersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;

        public GetExaminationsCreateRequestHandler(IExamCentersSQLRepository examCentersSQLRepository,
            ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _examCentersSQLRepository = examCentersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExaminationsCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            var vm = new ExaminationCreateViewModel();
            vm.ExamCenterItems = _examCentersSQLRepository.ExamCenterSelectList();
            vm.TrainingCenterItems = _trainingCentersSQLRepository.TrainingCenterSelectList();

            return await Task.FromResult(request.Controller.View(vm));
        }
    }
}
