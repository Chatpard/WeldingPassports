using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ListExamCenter : IDomainModel
    {
        public int ID { get; set; }
        public int ExamCenterID { get; set; }
        public int CompanyContactID { get; set; }

        // Navigation Properties
        public ExamCenter ExamCenter { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}
