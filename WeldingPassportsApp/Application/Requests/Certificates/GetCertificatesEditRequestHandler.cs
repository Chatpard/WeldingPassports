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
    public class GetCertificatesEditRequestHandler : IRequestHandler<GetCertificatesEditRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public GetCertificatesEditRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Handle(GetCertificatesEditRequest request, CancellationToken cancellationToken)
        {
            if (request.Controller.Url.IsLocalUrl(request.ReturnUrl))
                request.Controller.ViewBag.ReturnUrl = request.ReturnUrl;

            var vm = await _repository.GetCertificateEditAsync(request.EncryptedID);

            return request.Controller.View(vm);
        }
    }
}
