using Application.ViewModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.SQL
{
    public interface ICertificatesSQLRepository
    {
        Task PostCertificateCreateAsync(CertificateCreateViewModel vm, CancellationToken cancellationToken);
        Task<CertificateCreateViewModel> GetCertificateCreateAsync(string examinationEncryptedID);
        Task PostCertificateEditAsync(CertificateEditViewModel vm, CancellationToken cancellationToken);
        Task PostCertificateUpdateAsync(string registrationEncryptedID, bool? HasPassed, CancellationToken cancellationToken);
        Task<CertificateEditViewModel> GetCertificateEditAsync(string encryptedID);
        Task<CertificateDetailsViewModel> GetCertificateDetailsAsync(string encryptedID);
        Task<EntityEntry<Registration>> DeleteByEncryptedID(string encryptedID);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
