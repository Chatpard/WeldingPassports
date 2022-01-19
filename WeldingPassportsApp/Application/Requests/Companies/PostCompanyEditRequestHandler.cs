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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Companies
{
    public class PostCompanyContactEditRequestHandler : IRequestHandler<PostCompanyEditRequest, IActionResult>
    {
        private readonly ICompanyContactsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostCompanyContactEditRequestHandler(ICompanyContactsSQLRepository repository, IMapper mapper)
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

                CompanyContact company = _mapper.Map<CompanyContact>(request.CompanyChanges);
                _repository.PostCompanyContactEdit(company);
                await _repository.SaveAsync(cancellationToken);
                
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
