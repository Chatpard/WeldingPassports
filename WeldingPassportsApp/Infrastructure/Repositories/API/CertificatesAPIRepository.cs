using Application.APIModels;
using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.API
{
    public class CertificatesAPIRepository : ICertificationsAPIRepository
    {
        private readonly AppDbContext _context;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly ICertificatesSQLRepository _certificatesSQLRepository;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IDataProtectionPurposeStrings _dataProtectionPurposeStrings;
        private readonly IDataProtector _protector;

        public CertificatesAPIRepository(AppDbContext context, IAppSettingsSQLRepository appSettingsSQLRepository, ICertificatesSQLRepository certificatesSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _context=context;
            _appSettingsSQLRepository=appSettingsSQLRepository;
            _certificatesSQLRepository=certificatesSQLRepository;
            _dataProtectionProvider=dataProtectionProvider;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public int DeleteRevokeByEncryptedID(string encryptedID)
        {
            _certificatesSQLRepository.DeleteByEncryptedID(encryptedID);
            return _context.SaveChanges();
        }

        public async Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromPEPassport(int? pePassportID, int? processID, DateTime examDate)
        {
            return await _certificatesSQLRepository.GetRegistrationTypesSelectListFromPEPassport(pePassportID, processID, examDate);
        }

        public async Task<GetProcessNamesReponse> GetProcessNamesSelectList(string examinationEncryptedID, int? pePassportID, int? registrationID)
        {
            int? examinationDecryptedID = Convert.ToInt32(_protector.Unprotect(examinationEncryptedID));
            return new GetProcessNamesReponse()
            {
                ProcessNamesSelectList = await _certificatesSQLRepository.GetProcessNamesSelectList(examinationDecryptedID, pePassportID, registrationID)
            };
        }

        public async Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID)
        {
            return await _certificatesSQLRepository.GetCertificateMaxExpirationDate(pePassportID, processID);
        }
    }
}