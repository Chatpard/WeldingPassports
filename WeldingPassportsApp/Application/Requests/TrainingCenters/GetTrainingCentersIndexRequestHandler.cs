using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.TrainingCenters
{
    public class GetTrainingCentersIndexRequestHandler : IRequestHandler<GetTrainingCentersIndexRequest, IActionResult>
    {
        private readonly ITrainingCentersSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetTrainingCentersIndexRequestHandler(ITrainingCentersSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetTrainingCentersIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "CompanyName_asc";
            request.Controller.ViewData["CompanyName"] = request.SortOrder == "CompanyName_desc" ? "CompanyName_asc" : "CompanyName_desc";
            request.Controller.ViewData["BusinessAddressPostalCode"] = request.SortOrder == "BusinessAddressPostalCode_desc" ? "BusinessAddressPostalCode_asc" : "BusinessAddressPostalCode_desc";
            request.Controller.ViewData["BusinessAddressCity"] = request.SortOrder == "BusinessAddressCity_desc" ? "BusinessAddressCity_asc" : "BusinessAddressCity_desc";
            request.Controller.ViewData["Contact"] = request.SortOrder == "Contact_desc" ? "Contact_asc" : "Contact_desc";
            request.Controller.ViewData["Email"] = request.SortOrder == "Email_desc" ? "Email_asc" : "Email_desc";
            request.Controller.ViewData["MobilePhone"] = request.SortOrder == "MobilePhone_desc" ? "MobilePhone_asc" : "MobilePhone_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetTrainingCentersIndexPaginatedAsync(7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
