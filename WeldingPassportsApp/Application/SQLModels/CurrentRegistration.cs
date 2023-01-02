using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class CurrentRegistration
    {
        public int? RegistrationTypeID { get; set; }

        public int? ProcessID { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public Examination Examination { get; set; }

        public bool? HasPassed { get; set; }

        public Revoke Revoke { get; set; }
    }
}
