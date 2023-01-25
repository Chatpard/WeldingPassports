using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
//using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.ExamCenters
{
    public class GetExamCentersIndexRequestHandler : IRequestHandler<GetExamCentersIndexRequest, IActionResult>
    {
        private readonly IExamCentersSQLRepository _repository;
        private readonly IMapper _mapper;

        public GetExamCentersIndexRequestHandler(IExamCentersSQLRepository repository,IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(GetExamCentersIndexRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.TempData.ContainsKey("ModelStateError"))
            {
                var modelStateError = JsonSerializer.Deserialize<ModelStateErr>((string) request.Controller.TempData["ModelStateError"]);
                
                request.Controller.ModelState.AddModelError(modelStateError.Key, modelStateError.ErrorMessage);
                request.Controller.ModelState.AddModelError(modelStateError.Key, modelStateError.ErrorMessage);
            }

            request.Controller.ViewData["CurrentSort"] = request.SortOrder ?? "CompanyName_asc";
            request.Controller.ViewData["CompanyName"] = request.SortOrder == "CompanyName_desc" ? "CompanyName_asc" : "CompanyName_desc";
            request.Controller.ViewData["IsActive"] = request.SortOrder == "IsActive_desc" ? "IsActive_asc" : "IsActive_desc";

            if (request.SearchString != null)
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentFilter;

            request.Controller.ViewData["CurrentFilter"] = request.SearchString;

            request.Controller.ViewBag.CurrentUrl = request.Controller.Request.GetEncodedPathAndQuery();

            IPaginatedList<ExamCenterIndexViewModel> vm = await _repository.GetExamCentersIndexPaginatedAsync(7, request.PageNumber ?? 1, request.SearchString, request.SortOrder);

            return request.Controller.View(vm);
        }
        private class ModelStateErr
        {
            public string Key { get; set; }

            public string ErrorMessage { get; set; }
        }
    }
}
