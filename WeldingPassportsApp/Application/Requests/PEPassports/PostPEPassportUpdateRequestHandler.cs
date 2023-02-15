using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.PEPassports
{
    public class PostPEPassportUpdateRequestHandler : IRequestHandler<PostPEPassportUpdateRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;
        private readonly IMapper _mapper;

        public PostPEPassportUpdateRequestHandler(ICertificatesSQLRepository repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper=mapper;
        }

        public async Task<IActionResult> Handle(PostPEPassportUpdateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                foreach(PEPassportDetailsRegistrationsIndexViewModel registrationIndexViewModel in request.PePassportUpdates.Certifications)
                {
                    if(!registrationIndexViewModel.HasNext)
                    {
                        await _repository.PostCertificateUpdateAsync(registrationIndexViewModel.EncryptedID, registrationIndexViewModel.HasPassed, cancellationToken);
                    }
                }

                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.PePassportUpdates);
        }
    }
}
