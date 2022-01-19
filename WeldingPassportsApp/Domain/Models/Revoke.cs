using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Revoke : IDomainModel
    {
        public int ID { get; set; }
        public int RegistrationID { get; set; }
        public int CompanyContactID { get; set; }
        private DateTime? revokeDay;
        public DateTime? RevokeDay
        {
            get
            {
                if (revokeDay != null)
                    return revokeDay.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    revokeDay = value.Value.Date;
                else
                    revokeDay = null;
            }
        }
        public string Comment { get; set; }

        // Navigation Properties
        public Registration Registration { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}
