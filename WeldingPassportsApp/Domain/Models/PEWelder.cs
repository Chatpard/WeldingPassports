using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PEWelder : IDomainModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalNumber { get; set; }
        public string IdNumber { get; set; }
        public string PhotoPath { get; set; }

        // Navigation Properties
        public ICollection<PEPassport> PEPassports { get; set; }
    }
}
