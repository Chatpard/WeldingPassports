using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Address : IDomainModel
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(1024)")]
        public string BusinessAddress { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string BusinessAddressPostalCode { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string BusinessAddressCity { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string BusinessAddressCountry { get; set; }

        // Navigation Properties
        public ICollection<Company> Companies { get; set; }
        public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}

// Field lengths https://learn.microsoft.com/en-us/microsoft-365/enterprise/add-several-users-at-the-same-time?view=o365-worldwide
