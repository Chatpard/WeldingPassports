using Application.Interfaces.Repositories.SQL;
using Application.Requests.PEPassports;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyContactCreateRequestHandler : IRequestHandler<PostCompanyContactCreateRequest, IActionResult>
    {
        private readonly ICompanyContactsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostCompanyContactCreateRequestHandler(ICompanyContactsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostCompanyContactCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                CompanyContact companyContact = _mapper.Map<CompanyContact>(request.ContactVM);
                await _repository.PostCompanyContactCreateAsync(companyContact);
                await _repository.SaveAsync(cancellationToken);
                
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
