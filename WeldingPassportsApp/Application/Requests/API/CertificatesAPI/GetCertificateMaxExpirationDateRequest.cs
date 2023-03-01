using Application.APIModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetCertificateMaxExpirationDateRequest: IRequest<ExpiryDateAPIModel>
    {
        public GetCertificateMaxExpirationDateRequest(int? pePasportID, int? processID, int? registrationTypeID, string examDateString)
        {
            PePasportID = pePasportID;

            ProcessID = processID;

            RegistrationTypeID = registrationTypeID;

            try
            {
                ExamDate = DateTime.Parse(examDateString);
            }
            catch { }
        }

        public int? PePasportID { get; }
        public int? ProcessID { get; }
        public int? RegistrationTypeID { get; }
        public DateTime? ExamDate { get; }
    }
}
