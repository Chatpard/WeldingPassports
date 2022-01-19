using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Certificates
{
    public class PostCertificatesEditRequestHandler : IRequestHandler<PostCertificatesEditRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public PostCertificatesEditRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(PostCertificatesEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            if (request.VM.CurrentCertificateRevokedByCompanyContactID == 0)
                request.VM.CurrentCertificateRevokedByCompanyContactID = null;
            
            if (request.Controller.ModelState.IsValid)
            {
                await _repository.CertificateUpdateAsync(request.VM, cancellationToken);

                return request.Controller.LocalRedirect(request.ReturnUrl);
            }

            return request.Controller.View();
        }
    }
}
