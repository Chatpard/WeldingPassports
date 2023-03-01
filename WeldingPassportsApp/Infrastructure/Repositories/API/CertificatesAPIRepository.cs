using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Application.SQLModels;
using Infrastructure.Services.Persistence;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.API
{
    public class CertificatesAPIRepository : ICertificationsAPIRepository
    {
        private readonly AppDbContext _context;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;
        private readonly ICertificatesSQLRepository _certificatesSQLRepository;

        public CertificatesAPIRepository(AppDbContext context, IAppSettingsSQLRepository appSettingsSQLRepository, ICertificatesSQLRepository certificatesSQLRepository)
        {
            _context=context;
            _appSettingsSQLRepository=appSettingsSQLRepository;
            _certificatesSQLRepository=certificatesSQLRepository;
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

        public async Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID)
        {
            return await _certificatesSQLRepository.GetCertificateMaxExpirationDate(pePassportID, processID);
        }
    }
}