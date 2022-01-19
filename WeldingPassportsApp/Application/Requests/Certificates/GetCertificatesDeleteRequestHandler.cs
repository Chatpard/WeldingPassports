using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Certificates
{
    public class GetCertificatesDeleteRequestHandler : IRequestHandler<GetCertificatesDeleteRequest, IActionResult>
    {

        public GetCertificatesDeleteRequestHandler()
        {

        }

        public Task<IActionResult> Handle(GetCertificatesDeleteRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
