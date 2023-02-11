using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    public class GetExaminationsIndexRequestHandler : IRequestHandler<GetExaminationsIndexRequest, IActionResult>
    {
        private readonly IExaminationsSQLRepository _repository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;
        private readonly IExamCentersSQLRepository _examCentersSQLRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetExaminationsIndexRequestHandler(IExaminationsSQLRepository repository, ITrainingCentersSQLRepository trainingCentersSQLRepository, IExamCentersSQLRepository examCentersSQLRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
            _examCentersSQLRepository = examCentersSQLRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        private const string EncryptedExaminationID = "EncryptedExaminationID";
        private const string ReturnUrl = "ReturnUrl";

        public async Task<IActionResult> Handle(GetExaminationsIndexRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.TempData.ContainsKey(EncryptedExaminationID) && request.Controller.TempData.ContainsKey(ReturnUrl))
            {
                if (request.Controller.TempData[EncryptedExaminationID] != null && request.Controller.TempData[ReturnUrl] != null)
                {
                    string encryptedExaminationID = (string) request.Controller.TempData[EncryptedExaminationID];
                    request.Controller.TempData.Remove(EncryptedExaminationID);
                    string returnUrl = (string) request.Controller.TempData[ReturnUrl];
                    request.Controller.TempData.Remove(ReturnUrl);
                    return request.Controller.RedirectToAction(request.ExaminationDetailsActionName, request.ExaminationsControllerName, new { id = encryptedExaminationID, returnUrl = returnUrl });
                }
            }

            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "ExamDate_asc";
            request.Controller.ViewData["ExamDateSort"] = request.SortOrder == "ExamDate_desc" ? "ExamDate_asc" : "ExamDate_desc";
            request.Controller.ViewData["CompanyNameTCSort"] = request.SortOrder == "CompanyNameTC_desc" ? "CompanyNameTC_asc" : "CompanyNameTC_desc";
            request.Controller.ViewData["CompanyNameECSort"] = request.SortOrder == "CompanyNameEC_desc" ? "CompanyNameEC_asc" : "CompanyNameEC_desc"; 
            request.Controller.ViewData["NumberOfPassportsSort"] = request.SortOrder == "NumberOfPassports_desc" ? "NumberOfPassports_asc" : "NumberOfPassports_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            string userId = _userManager.GetUserId(request.Controller.User);
            TrainingCenter trainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
            int? trainingCenterId = trainingCenter?.ID;
            ExamCenter examCenter = await _examCentersSQLRepository.GetExamCenterByUserId(userId);
            int? examCenterId = examCenter?.ID;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetExaminationsIndexPaginatedAsync(7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder, trainingCenterId: trainingCenterId, examCenterId: examCenterId);

            return request.Controller.View(vm);
        }
    }
}
