using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class CurrentRegistration
    {
        public int ID { get; set; }

        public int ExaminationID { get; set; }

        public int? PEPassportID { get; set; }

        public int? RegistrationTypeID { get; set; }

        public int? ProcessID { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool? HasPassed { get; set; }

        public int? PreviousRegistrationID { get; set; }

        //Navigation Properties
        public Examination Examination { get; set; }

        public PEPassport PEPassport { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public Process Process { get; set; }

        public Company Company { get; set; }

        public Revoke Revoke { get; set; }
    }
}
