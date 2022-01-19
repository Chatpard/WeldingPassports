using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class GetExaminationEditRequestHandler : IRequestHandler<GetExaminationEditRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _examinationsSQLRepository;
        private readonly IExamCentersSQLRepository _examCentersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;

        public GetExaminationEditRequestHandler(IExaminationsSQLRepository examinationsSQLRepository,
            IExamCentersSQLRepository examCentersSQLRepository,
            ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _examinationsSQLRepository = examinationsSQLRepository;
            _examCentersSQLRepository = examCentersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExaminationEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _examinationsSQLRepository.GetExaminationEditAsync(request.EncryptedID);
            vm.TrainingCenterItems = _trainingCentersSQLRepository.TrainingCenterSelectList();
            vm.ExamCenterItems = _examCentersSQLRepository.ExamCenterSelectList();

            return request.Controller.View(vm);
        }
    }
}
