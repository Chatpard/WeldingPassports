using Application.Interfaces.Repositories.SQL;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Certificates
{
    public class GetCertificatesCreateRequestHandler : IRequestHandler<GetCertificatesCreateRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public GetCertificatesCreateRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetCertificatesCreateRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            var vm = await _repository.GetCertificateCreateAsync(request.ExaminationEncryptedID);

            return request.Controller.View(vm);
        }
    }
}
