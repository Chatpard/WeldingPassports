using Application.APIModels;
using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Application.Security;
using Application.SQLModels;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.API
{
    public class CertificatesAPIRepository : ICertificationsAPIRepository
    {
        private readonly AppDbContext _context;
        private readonly ICertificatesSQLRepository _certificatesSQLRepository;
        private readonly IDataProtector _protector;

        public CertificatesAPIRepository(AppDbContext context, ICertificatesSQLRepository certificatesSQLRepository,
            IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _context=context;
            _certificatesSQLRepository=certificatesSQLRepository;
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

        public async Task<int?> GetCompanyIDByPEPassport(int? pePassportID)
        {
            var registration = await _context.Registrations
                .Include(registration => registration.PEPassport)
                .Where(registration => registration.PEPassport.PEWelderID == _context.PEPassports
                    .Where(pePassport => pePassport.ID == pePassportID)
                    .SingleOrDefault().PEWelderID)
                .Include(registration => registration.Examination)
                .OrderByDescending(registration => registration.Examination.ExamDate)
                .FirstOrDefaultAsync();

            return registration?.CompanyID;
        }

        public async Task<bool?> GetHasNotSet(int? pePassportID, int? processID)
        {
            return _context.Registrations
                .Include(certificate => certificate.PEPassport)
                .Where(certificate => certificate.PEPassport.PEWelderID == _context.PEPassports
                    .Where(pePassport => pePassport.ID == pePassportID).SingleOrDefault().PEWelderID)
                .Where(certificate => certificate.ProcessID == processID)
                .Any(certificate => certificate.HasPassed == null);
        }
    }
}