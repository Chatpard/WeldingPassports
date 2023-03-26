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

namespace Application.Requests.CompanyContacts
{
    public class GetCompanyContactsIndexRequestHandler : IRequestHandler<GetCompanyContactsIndexRequest, IActionResult>
    {
        private readonly IContactsSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetCompanyContactsIndexRequestHandler(IContactsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetCompanyContactsIndexRequest request, CancellationToken cancellationToken)
        {
            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "FirstName_asc";
            request.Controller.ViewData["FirstName"] = request.SortOrder == "FirstName_desc" ? "FirstName_asc" : "FirstName_desc";
            request.Controller.ViewData["LastName"] = request.SortOrder == "LastName_desc" ? "LastName_asc" : "LastName_desc";
            request.Controller.ViewData["Email"] = request.SortOrder == "Email_desc" ? "Email_asc" : "Email_desc";
            request.Controller.ViewData["RoleName"] = request.SortOrder == "RoleName_desc" ? "RoleName_asc" : "RoleName_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            var vm = await _repository.GetContactsIndexPaginatedAsync(7, request.PageNumber ?? 1,
                request.SearchString, request.SortOrder);

            request.Controller.TempData.Clear();

            return request.Controller.View(vm);
        }
    }
}
