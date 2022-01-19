using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    public class PostPEPassportCreateRequestHandler : IRequestHandler<PostPEPassportCreateRequest, IActionResult>
    {
        private readonly IPEPassportsSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostPEPassportCreateRequestHandler(IPEPassportsSQLRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(PostPEPassportCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                PEPassport pePassport = _mapper.Map<PEPassport>(request.PEPassportVM);

                await _repository.PostPEPassportCreateAsync(pePassport);
                await _repository.SaveAsync(cancellationToken);
                PEPassportEditViewModel newPEPassport = _mapper.Map <PEPassportEditViewModel>(pePassport);

                return request.Controller.RedirectToAction(request.NameOfDetailsAction, new { id = newPEPassport.EncryptedID, returnUrl = request.ReturnUrl });
            }

            return request.Controller.View();
        }
    }
}
