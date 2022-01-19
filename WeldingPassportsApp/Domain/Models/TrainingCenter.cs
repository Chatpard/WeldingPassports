using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class TrainingCenter : IDomainModel
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public char Letter { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public Company Company { get; set; }
        public ICollection<Examination> Examinations { get; set; }
        public ListTrainingCenter ListTrainingCenter { get; set; }
        public ICollection<PEPassport> PEPassports { get; set; }
    }
}
