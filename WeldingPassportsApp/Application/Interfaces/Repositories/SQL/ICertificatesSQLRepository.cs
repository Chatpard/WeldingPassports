using Application.ViewModels;
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
        Task CertificateUpdateAsync(CertificateEditViewModel vm, CancellationToken cancellationToken);
        Task<CertificateEditViewModel> GetCertificateEditAsync(string encryptedID);
        Task<CertificateDetailsViewModel> GetCertificateDetailsAsync(string encryptedID);
    }
}
