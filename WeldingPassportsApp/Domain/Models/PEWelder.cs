using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class PEWelder : IDomainModel
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string LastName { get; set; }
        
        [Column(TypeName = "varchar(64)")]
        public string NationalNumber { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string IdNumber { get; set; }
        
        [Column(TypeName = "varchar(256)")]
        public string PhotoPath { get; set; }

        // Navigation Properties
        public ICollection<PEPassport> PEPassports { get; set; }
    }
}
