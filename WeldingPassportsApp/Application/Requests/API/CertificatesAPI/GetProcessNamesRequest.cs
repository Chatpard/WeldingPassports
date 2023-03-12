using Application.APIModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetProcessNamesRequest: IRequest<GetProcessNamesReponse>
    {
        public GetProcessNamesRequest(string examinationEncryptedID, int? pePassportID, int? registrationID)
        {
            PePassportID=pePassportID;
            ExaminationEncryptedID=examinationEncryptedID;
            RegistrationID=registrationID;
        }

        public string ExaminationEncryptedID { get; }
        public int? PePassportID { get; }
        public int? RegistrationID { get; }
    }
}
