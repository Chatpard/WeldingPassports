using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Address : IDomainModel
    {
        public int ID { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessAddressPostalCode { get; set; }
        public string BusinessAddressCity { get; set; }
        public string BusinessAddressCountry { get; set; }

        // Navigation Properties
        public ICollection<Company> Companies { get; set; }
        public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
