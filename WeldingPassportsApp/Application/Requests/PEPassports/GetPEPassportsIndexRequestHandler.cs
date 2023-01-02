using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportsIndexRequestHandler : IRequestHandler<GetPEPassportsIndexRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _pePassportsSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public GetPEPassportsIndexRequestHandler(IPEPassportsSQLRepository pePassportsSQLRepository, ITrainingCentersSQLRepository trainingCentersSQLRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _pePassportsSQLRepository = pePassportsSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(GetPEPassportsIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "AVNumber_asc";
            request.Controller.ViewData["AVNumberSort"] = request.SortOrder == "AVNumber_desc" ? "AVNumber_asc" : "AVNumber_desc";
            request.Controller.ViewData["FirstNameSort"] = request.SortOrder == "FirstName_desc" ? "FirstName_asc" : "FirstName_desc";
            request.Controller.ViewData["LastNameSort"] = request.SortOrder == "LastName_desc" ? "LastName_asc" : "LastName_desc";
            request.Controller.ViewData["CompanyNameSort"] = request.SortOrder == "CompanyName_desc" ? "CompanyName_asc" : "CompanyName_desc";
            request.Controller.ViewData["ExpiryDateSort"] = request.SortOrder == "ExpiryDate_desc" ? "ExpiryDate_asc" : "ExpiryDate_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            string userId = _userManager.GetUserId(request.Controller.User);
            TrainingCenter trainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
            int? trainingCenterId = trainingCenter?.ID;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _pePassportsSQLRepository.GetPEPassportsIndexPaginatedAsync(trainingCenterId, 7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
