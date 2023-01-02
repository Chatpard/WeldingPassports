using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class GetGetRegistrationTypesFromPEPassportReponse
    {
        public int? CompanyID { get; set; }

        public int? ProcessID { get; set; }

        public SelectList RegistrationsSelectList { get; set; }
    }
}
