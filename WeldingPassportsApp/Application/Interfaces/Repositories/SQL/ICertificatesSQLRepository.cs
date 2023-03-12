using Application.SQLModels;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromPEPassport(int? pePassportID, int? processID, DateTime examDate, Registration previousRegistration = null);
        Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID, DateTime? examDate = null);
        Task<SelectList> GetProcessNamesSelectList(int? examinationEncryptedID, int? pePassportID, int? registrationID);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
