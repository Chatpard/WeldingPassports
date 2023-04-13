using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class TrainingCenter : IDomainModel
    {
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public int? CompanyContactID { get; set; }

        [Column(TypeName = "varchar(1)")]
        public char Letter { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public Company Company { get; set; }

        public CompanyContact CompanyContact { get; set; }

        public ICollection<Examination> Examinations { get; set; }

        public ICollection<PEPassport> PEPassports { get; set; }
    }
}
