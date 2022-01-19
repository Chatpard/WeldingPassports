using Application.Interfaces.Repositories.SQL;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.CompanyContacts
{
    public class PostCompanyCreateRequestHandler : IRequestHandler<PostCompanyCreateRequest, IActionResult>
    {
        private readonly ICompaniesSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostCompanyCreateRequestHandler(ICompaniesSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostCompanyCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                Company company = _mapper.Map<Company>(request.ContactCreateVM);
                await _repository.PostCompanyCreateAsync(company);
                await _repository.SaveAsync(cancellationToken);
                
                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
