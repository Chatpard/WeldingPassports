using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    public class DeleteCertificatesRevokeRequestHandler : IRequestHandler<DeleteCertificatesRevokeRequest, EntityEntry<Revoke>>
    {
        private readonly IRevokeSQLRepository _repository;

        public DeleteCertificatesRevokeRequestHandler(IRevokeSQLRepository repository)
        {
            _repository=repository;
        }

        public async Task<EntityEntry<Revoke>> Handle(DeleteCertificatesRevokeRequest request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteByCertificateEncryptedID(request.CertificateEncryptedID);
        }
    }
}
