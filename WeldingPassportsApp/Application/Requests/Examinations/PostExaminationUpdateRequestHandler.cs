using Application.Interfaces.Repositories.SQL;
using Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Examinations
{
    internal class PostExaminationUpdateRequestHandler : IRequestHandler<PostExaminationUpdateRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public PostExaminationUpdateRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(PostExaminationUpdateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.ModelState.IsValid)
            {
                if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                    request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

                foreach(ExaminationDetailsCertificationsIndexViewModel registrationIndexViewModel in request.ExaminationUpdates.Certifications)
                {
                    if (!registrationIndexViewModel.HasNext)
                    {
                        await _repository.PostCertificateUpdateAsync(registrationIndexViewModel.EncryptedID, registrationIndexViewModel.HasPassed, cancellationToken);
                    }
                }

                await _repository.SaveChangesAsync(cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View(request.ExaminationUpdates);
        }
    }
}
