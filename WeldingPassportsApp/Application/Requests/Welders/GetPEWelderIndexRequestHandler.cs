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

namespace Application.Requests.Welders
{
    public class GetPEWelderIndexRequestHandler : IRequestHandler<GetPEWelderIndexRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public GetPEWelderIndexRequestHandler(IPEWeldersSQLRepository peWeldersSQLRepository, ITrainingCentersSQLRepository trainingCentersSQLRepository, UserManager<IdentityUser> userManager)
        {
            _peWeldersSQLRepository = peWeldersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(GetPEWelderIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "FirstName_asc";
            request.Controller.ViewData["FirstNameSort"] = request.SortOrder == "FirstName_desc" ? "FirstName_asc" : "FirstName_desc";
            request.Controller.ViewData["LastNameSort"] = request.SortOrder == "LastName_desc" ? "LastName_asc" : "LastName_desc";
            request.Controller.ViewData["AVNumberSort"] = request.SortOrder == "AVNumber_desc" ? "AVNumber_asc" : "AVNumber_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            string userId = _userManager.GetUserId(request.Controller.User);
            TrainingCenter trainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
            int? trainingCenterId = trainingCenter?.ID;
            
            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _peWeldersSQLRepository.GetPEWeldersIndexPaginatedAsync(trainingCenterId, 7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
