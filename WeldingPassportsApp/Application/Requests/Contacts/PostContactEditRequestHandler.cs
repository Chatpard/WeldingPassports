using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Contacts
{
    internal class PostContactEditRequestHandler : MediatR.IRequestHandler<PostContactEditRequest, IActionResult>
    {
        private readonly IContactsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostContactEditRequestHandler(IContactsSQLRepository repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }
        public async Task<IActionResult> Handle(PostContactEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                Contact contact = _mapper.Map<Contact>(request.ContactChanges);
                _repository.PostContactEdit(contact);
                await _repository.SaveAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }

    }
}
