using Application.SQLModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetRegistrationTypesFromPEPassportRequest : IRequest<ActionResult<GetGetRegistrationTypesFromPEPassportReponse>>
    {
        public GetRegistrationTypesFromPEPassportRequest(int pePassortID, int? processID, DateTime examDate)
        {
            PEPassortID=pePassortID;
            ProcessID=processID;
            ExamDate=examDate;
        }

        public int PEPassortID { get; }
        public int? ProcessID { get; }
        public DateTime ExamDate { get; }
    }
}