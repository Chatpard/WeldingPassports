using Application.Interfaces.Repositories.SQL;
using Application.Requests.PEPassports;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Companies
{
    public class PostCompanyContactEditRequestHandler : IRequestHandler<PostCompanyEditRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostCompanyContactEditRequestHandler(ICompaniesSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostCompanyEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                Company company = _mapper.Map<Company>(request.CompanyChanges);
                _repository.PostCompanyEdit(company);
                await _repository.SaveAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
