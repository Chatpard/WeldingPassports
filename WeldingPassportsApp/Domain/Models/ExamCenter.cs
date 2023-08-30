using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ExamCenter : IDomainModel
    {
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public int? CompanyContactID { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public Company Company { get; set; } = null!;

        public CompanyContact CompanyContact { get; set; }

        public ICollection<Examination> Examinations { get; set; }
    }
}
