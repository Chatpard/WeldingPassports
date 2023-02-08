﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.API.CertificatesAPI
{
    public class GetCertificateMaxExpirationDateRequest: IRequest<DateTime?>
    {
        public GetCertificateMaxExpirationDateRequest(int? pePasportID, int? processID, string examDateString)
        {
            PePasportID = pePasportID;
            ProcessID = processID;
            try
            {
                ExamDate = DateTime.Parse(examDateString);
            }
            catch { }
        }

        public int? PePasportID { get; }
        public int? ProcessID { get; }
        public DateTime? ExamDate { get; }
    }
}
