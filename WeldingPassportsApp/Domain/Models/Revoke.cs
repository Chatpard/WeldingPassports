using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Revoke : IDomainModel
    {
        public int ID { get; set; }
        public int RegistrationID { get; set; }
        public int CompanyContactID { get; set; }
        private DateTime? revokeDate;
        public DateTime? RevokeDate
        {
            get
            {
                if (revokeDate != null)
                    return revokeDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    revokeDate = value.Value.Date;
                else
                    revokeDate = null;
            }
        }

        [Column(TypeName = "varchar(1024)")]
        public string Comment { get; set; }

        // Navigation Properties
        public Registration Registration { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}
