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
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetExaminationsIndexRequestHandler(IExaminationsSQLRepository repository, ITrainingCentersSQLRepository trainingCentersSQLRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(GetExaminationsIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "ExamDate_asc";
            request.Controller.ViewData["ExamDateSort"] = request.SortOrder == "ExamDate_desc" ? "ExamDate_asc" : "ExamDate_desc";
            request.Controller.ViewData["CompanyNameSort"] = request.SortOrder == "CompanyName_desc" ? "CompanyName_asc" : "CompanyName_desc";
            request.Controller.ViewData["NumberOfPassportsSort"] = request.SortOrder == "NumberOfPassports_desc" ? "NumberOfPassports_asc" : "NumberOfPassports_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            string userId = _userManager.GetUserId(request.Controller.User);
            TrainingCenter trainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
            int? trainingCenterId = trainingCenter?.ID;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetExaminationsIndexPaginatedAsync(trainingCenterId, 7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);  ;

            return request.Controller.View(vm);
        }
    }
}
