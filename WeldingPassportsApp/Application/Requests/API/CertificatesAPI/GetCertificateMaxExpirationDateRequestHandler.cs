using Application.APIModels;
using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetCertificateMaxExpirationDateRequestHandler : IRequestHandler<GetCertificateMaxExpirationDateRequest, ExpiryDateAPIModel>
    {
        private readonly ICertificationsAPIRepository _certificationsAPIRepository;
        private readonly IAppSettingsSQLRepository _appSettingsSQLRepository;

        public GetCertificateMaxExpirationDateRequestHandler(ICertificationsAPIRepository certificationsAPIRepository, IAppSettingsSQLRepository appSettingsSQLRepository)
        {
            _certificationsAPIRepository = certificationsAPIRepository;
            _appSettingsSQLRepository=appSettingsSQLRepository;
        }
        public async Task<ExpiryDateAPIModel> Handle(GetCertificateMaxExpirationDateRequest request, CancellationToken cancellationToken)
        {
            if(request.ExamDate == null)
            {
                return new ExpiryDateAPIModel();
            }

            AppSettings appSettings = await _appSettingsSQLRepository.GetAppsetingsAsync();

            if (appSettings == null)
            {
                return new ExpiryDateAPIModel();
            }

            ExpiryDateAPIModel expiryDateAPIModel = new ExpiryDateAPIModel()
            {
                ExpiryDate = request.ExamDate?.AddDays(appSettings.MaxExpiryDays).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                MinExpiryDate = request.ExamDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                MaxExpiryDate = request.ExamDate?.AddDays(appSettings.MaxExpiryDays).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            };

            expiryDateAPIModel.ExpiryDateErrorMessage = ErrorMessage("Epiry Date", expiryDateAPIModel.MinExpiryDate, expiryDateAPIModel.MaxExpiryDate);
            expiryDateAPIModel.RevokeDateErrorMessage = ErrorMessage("Revoke Date", expiryDateAPIModel.MinExpiryDate, expiryDateAPIModel.MaxExpiryDate);

            //Todo hardcoded
            if (request.RegistrationTypeID == 1)
            {
                return expiryDateAPIModel;
            }

            DateTime? expirationDate = await _certificationsAPIRepository?.GetCertificateMaxExpirationDate(request.PePasportID, request.ProcessID);

            if(expirationDate == null)
            {
                return expiryDateAPIModel;
            }

            DateTime? maxExtensionDays = expirationDate?.AddDays(appSettings.MaxExtensionDays);
            DateTime? maxInAdvanceDays = expirationDate?.AddDays(appSettings.MaxInAdvanceDays * -1);
            if(maxInAdvanceDays > request.ExamDate || request.ExamDate > maxExtensionDays)
            {
                expiryDateAPIModel.ExpiryDate = expirationDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                expiryDateAPIModel.MaxExpiryDate = expirationDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                expiryDateAPIModel.ExpiryDateErrorMessage = ErrorMessage("Epiry Date", expiryDateAPIModel.MinExpiryDate, expiryDateAPIModel.MaxExpiryDate);
                expiryDateAPIModel.RevokeDateErrorMessage = ErrorMessage("Revoke Date", expiryDateAPIModel.MinExpiryDate, expiryDateAPIModel.MaxExpiryDate);
            }

            return expiryDateAPIModel;
        }

        private string ErrorMessage(string dateName, string minExpiryISODate, string maxExpiryISODate)
        {
            string minExpiryDate = "", maxExpiryDate = "";
            try
            {
                minExpiryDate = Convert.ToDateTime(minExpiryISODate).ToString("dd-MM-yyyy",CultureInfo.InvariantCulture);
                maxExpiryDate = Convert.ToDateTime(maxExpiryISODate).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            
            if (minExpiryISODate != "")
            {
                String.Format("{0} needs to be later then {1}", dateName, minExpiryDate);
            }

            if (maxExpiryISODate == "")
            {
                String.Format("{0} needs to be earlier then {1}", dateName, maxExpiryDate);
            }

            return String.Format("{0} needs to between {1} and {2}", dateName, minExpiryDate, maxExpiryDate);
        }
    }
}