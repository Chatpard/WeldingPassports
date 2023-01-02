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

                foreach(PEPassportDetailsRegistrationsIndexViewModel registrationViewModel in request.PePassportUpdates.Certifications)
                {
                    CertificateEditViewModel certificateViewModel = await _repository.GetCertificateEditAsync(registrationViewModel.EncryptedID);

                    if(certificateViewModel == null)
                    {
                        return request.Controller.View(request.PePassportUpdates);
                    }

                    if(certificateViewModel.CurrentCertificateHasPassed != registrationViewModel.HasPassed)
                    {
                        certificateViewModel.CurrentCertificateHasPassed = registrationViewModel.HasPassed;
                        await _repository.CertificateUpdateAsync(certificateViewModel, cancellationToken);
                    }

                }

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.PePassportUpdates);
        }
    }
}
