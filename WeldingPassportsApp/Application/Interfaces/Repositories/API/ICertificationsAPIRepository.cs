using Application.APIModels;
using Application.SQLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.API
{
    public interface ICertificationsAPIRepository
    {
        Task<GetRegistrationTypesFromPEPassportReponse> GetRegistrationTypesSelectListFromPEPassport(int? pePassportID, int? processID, DateTime examDate);
        int DeleteRevokeByEncryptedID(string pePassportID);
        Task<DateTime?> GetCertificateMaxExpirationDate(int? pePassportID, int? processID);
        Task<GetProcessNamesReponse> GetProcessNamesSelectList(string examinationEncryptedID, int? pePassportID, int? registrationID);
        Task<int?> GetCompanyIDByPEPassport(int? pePassportID);
        Task<bool?> GetHasNotSet(int? PePassportID, int? ProcessID);
    }
}