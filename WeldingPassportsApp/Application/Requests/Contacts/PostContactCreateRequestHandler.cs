using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Contacts
{
    class PostContactCreateRequestHandler : IRequestHandler<PostContactCreateRequest, IActionResult>
    {
        private readonly IContactsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostContactCreateRequestHandler(IContactsSQLRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostContactCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                Contact contact = _mapper.Map<Contact>(request.ContactCreateVM);
                await _repository.PostContactCreateAsync(contact);
                await _repository.SaveAsync(cancellationToken);

                request.Controller.TempData[nameof(CompanyContactEditViewModel.ContactID)] = contact.ID;

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
