using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    public class GetPEPassportsIndexRequestHandler : IRequestHandler<GetPEPassportsIndexRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetPEPassportsIndexRequestHandler(IPEPassportsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetPEPassportsIndexPaginatedAsync(7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
