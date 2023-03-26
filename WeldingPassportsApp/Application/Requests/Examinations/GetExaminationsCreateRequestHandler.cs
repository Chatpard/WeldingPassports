using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class GetExaminationsCreateRequestHandler 
        : IRequestHandler<GetExaminationsCreateRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _examCentersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;
        private readonly UserManager<AppUser> _userManager;

        public GetExaminationsCreateRequestHandler(IExamCentersSQLRepository examCentersSQLRepository,
            ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _examCentersSQLRepository = examCentersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
        }

        public async Task<IActionResult> Handle(GetExaminationsCreateRequest request, CancellationToken cancellationToken)
        {
            string userId = request.UserManager.GetUserId(request.Controller.User);
            TrainingCenter userTrainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
            int? userTrainingCenterId = userTrainingCenter?.ID;

            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = new ExaminationCreateViewModel();
            vm.ExamCenterItems = _examCentersSQLRepository.ExamCenterSelectList();
            vm.TrainingCenterItems = _trainingCentersSQLRepository.TrainingCenterSelectList(userTrainingCenterId);

            return await Task.FromResult(request.Controller.View(vm));
        }
    }
}
