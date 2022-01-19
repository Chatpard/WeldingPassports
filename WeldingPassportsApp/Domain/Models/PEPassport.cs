using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class PEPassport : IDomainModel
    {
        public int ID { get; set; }
        public int TrainingCenterID { get; set; }
        public int PEWelderID { get; set; }
        public string AVNumber { get; set; }

        // Navigation Properties
        public TrainingCenter TrainingCenter { get; set; }
        public PEWelder PEWelder { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
