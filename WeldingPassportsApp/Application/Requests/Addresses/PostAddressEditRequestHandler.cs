using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Addresses
{
    internal class PostAddressEditRequestHandler : IRequestHandler<PostAddressEditRequest, IActionResult>
    {
        private readonly IAddressesSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostAddressEditRequestHandler(IAddressesSQLRepository repository, IMapper mapper)
        {
            _repository=repository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(PostAddressEditRequest request, CancellationToken cancellationToken)
        {
            if(request.Controller.ModelState.IsValid)
            {
                if(request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                Address address = _mapper.Map<Address>(request.AddressChanges);
                _repository.PostAddressEdit(address);
                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
