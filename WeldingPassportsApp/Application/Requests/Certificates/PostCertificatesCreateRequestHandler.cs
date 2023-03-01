using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Certificates
{
    public class PostCertificatesCreateRequestHandler : IRequestHandler<PostCertificatesCreateRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public PostCertificatesCreateRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(PostCertificatesCreateRequest request, CancellationToken cancellationToken)
        {
            await _repository.PostCertificateCreateAsync(request.VM, cancellationToken);

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
