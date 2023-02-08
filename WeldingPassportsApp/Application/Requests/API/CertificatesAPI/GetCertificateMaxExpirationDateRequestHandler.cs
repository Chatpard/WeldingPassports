using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetCertificateMaxExpirationDateRequestHandler : IRequestHandler<GetCertificateMaxExpirationDateRequest, DateTime?>
    {
        private readonly ICertificationsAPIRepository _certificationsAPIRepository;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;

        public GetCertificateMaxExpirationDateRequestHandler(ICertificationsAPIRepository certificationsAPIRepository, IAppSettingsSQLRepository appSettingsSQLRepository)
        {
            _certificationsAPIRepository = certificationsAPIRepository;
            _appSettingsSQLRepository=appSettingsSQLRepository;
        }
        public async Task<DateTime?> Handle(GetCertificateMaxExpirationDateRequest request, CancellationToken cancellationToken)
        {
            DateTime? expirationDate = await _certificationsAPIRepository?.GetCertificateMaxExpirationDate(request.PePasportID, request.ProcessID);

            AppSettings appSettings = await _appSettingsSQLRepository.GetAppsetingsAsync();

            if(expirationDate != null)
            {
                DateTime? maxExtensionDays = expirationDate?.AddDays(appSettings.MaxExtensionDays);
                DateTime? maxInAdvanceDays = expirationDate?.AddDays(appSettings.MaxInAdvanceDays * -1);
                if(maxInAdvanceDays > request.ExamDate || request.ExamDate > maxExtensionDays)
                {
                    expirationDate = null;
                }
            }

            if(expirationDate == null)
            {
                expirationDate = request.ExamDate;
            }

            return expirationDate?.AddDays(appSettings.MaxExpiryDays);
        }
    }
}