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

namespace Application.Requests.Companies
{
    public class GetCompaniesIndexRequestHandler : IRequestHandler<GetCompaniesIndexRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetCompaniesIndexRequestHandler(ICompaniesSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetCompaniesIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "CompanyName_asc";
            request.Controller.ViewData["CompanyName"] = request.SortOrder == "CompanyName_desc" ? "CompanyName_asc" : "CompanyName_desc";
            request.Controller.ViewData["CompanyMainPhone"] = request.SortOrder == "CompanyMainPhone_desc" ? "CompanyMainPhone_asc" : "CompanyMainPhone_desc";
            request.Controller.ViewData["CompanyEmail"] = request.SortOrder == "CompanyEmail_desc" ? "CompanyEmail_asc" : "CompanyEmail_desc";
            request.Controller.ViewData["WebPage"] = request.SortOrder == "WebPage_desc" ? "WebPage_asc" : "WebPage_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetCompaniesIndexPaginatedAsync(7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
    }
}
