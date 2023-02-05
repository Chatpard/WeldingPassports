using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Registration : IDomainModel
    {
        public int ID { get; set; }
        public int? PreviousRegistrationID { get; set; }
        public int ExaminationID { get; set; }
        public int PEPassportID { get; set; }
        public int RegistrationTypeID { get; set; }
        public int ProcessID { get; set; }
        public int CompanyID { get; set; }
        private DateTime? expiryDate;
        public DateTime? ExpiryDate
        {
            get
            {
                if (expiryDate != null)
                    return expiryDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    expiryDate = value.Value.Date;
                else
                    expiryDate = null;
            }
        }
        public bool? HasPassed { get; set; }
        public string CertificatePath { get; set; }

        // Navigation Properties
        public Examination Examination { get; set; }

        public PEPassport PEPassport { get; set; }

        public RegistrationType RegistrationType { get; set; }

        public Process Process { get; set; }

        //[NotMapped]
        public Registration PreviousRegistration { get; set; }

        public Company Company { get; set; }

        public Revoke Revoke { get; set; }
    }
}
