using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class DeleteCertificatesRevokeRequest : IRequest<EntityEntry<Revoke>>
    {
        public DeleteCertificatesRevokeRequest(string certificateEncryptedID)
        {
            CertificateEncryptedID=certificateEncryptedID;
        }

        public string CertificateEncryptedID { get; }
    }
}
