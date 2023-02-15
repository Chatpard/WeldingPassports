using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.Certificates
{
    public class GetCertificatesDeleteRequestHandler : IRequestHandler<GetCertificatesDeleteRequest, IActionResult>
    {
        private readonly ICertificatesSQLRepository _repository;

        public GetCertificatesDeleteRequestHandler(ICertificatesSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<IActionResult> Handle(GetCertificatesDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteByEncryptedID(request.EncryptedID);
                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return request.Controller.LocalRedirect(request.ReturnUrl);
        }
    }
}
