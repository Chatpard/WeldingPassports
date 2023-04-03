using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ListTrainingCenter : IDomainModel
    {
        public int ID { get; set; }
        public int TrainingCenterID { get; set; }
        public int CompanyContactID { get; set; }

        // Navigation Properties
        public TrainingCenter TrainingCenter { get; set; }
        public CompanyContact CompanyContact { get; set; }
    }
}
//Todo: To be deleted