using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Welders
{
    public class GetPEWelderIndexRequestHandler : IRequestHandler<GetPEWelderIndexRequest, IActionResult>
    {
        private readonly IPEWeldersSQLRepository _peWeldersSQLRepository;
        private readonly ITrainingCentersSQLRepository _trainingCentersSQLRepository;

        public GetPEWelderIndexRequestHandler(IPEWeldersSQLRepository peWeldersSQLRepository, ITrainingCentersSQLRepository trainingCentersSQLRepository)
        {
            _peWeldersSQLRepository = peWeldersSQLRepository;
            _trainingCentersSQLRepository = trainingCentersSQLRepository;
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

            int? trainingCenterId = 0;
            if (request.Controller.User.IsInRole(RolesStore.TC))
            {
                string userId = request.UserManager.GetUserId(request.Controller.User);
                TrainingCenter trainingCenter = await _trainingCentersSQLRepository.GetTrainingCenterByUserId(userId);
                if (trainingCenter != null)
                {
                    trainingCenterId = trainingCenter?.ID;
                }
            }
            else if (request.Controller.User.IsInRole(RolesStore.Admin)
                || request.Controller.User.IsInRole(RolesStore.DSO)
                || request.Controller.User.IsInRole(RolesStore.EC))
            {
                trainingCenterId = null;
            }

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _peWeldersSQLRepository.GetPEWeldersIndexPaginatedAsync(trainingCenterId, 7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
