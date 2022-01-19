using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Examination : IDomainModel
    {
        public int ID { get; set; }
        public int TrainingCenterID { get; set; }
        public int ExamCenterID { get; set; }
        private DateTime? examDate;
        public DateTime? ExamDate
        {
            get
            {
                if (examDate != null)
                    return examDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    examDate = value.Value.Date;
                else
                    examDate = null;
            }
        }

        // Navigation Properties
        public TrainingCenter TrainingCenter { get; set; }
        public ExamCenter ExamCenter { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
