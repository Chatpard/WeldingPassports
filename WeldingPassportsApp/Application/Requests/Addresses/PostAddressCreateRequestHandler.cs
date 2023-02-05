using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Addresses
{
    class PostAddressCreateRequestHandler : IRequestHandler<PostAddressCreateRequest, IActionResult>
    {
        private readonly IAddressesSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostAddressCreateRequestHandler(IAddressesSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostAddressCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                Address address = _mapper.Map<Address>(request.AddressCreateVM);
                await _repository.PostAddressCreateAsync(address);
                await _repository.SaveChangesAsync(cancellationToken);

                request.Controller.TempData[nameof(CompanyContactEditViewModel.AddressID)] = address.ID;

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }
            return request.Controller.View();
        }
    }
}
