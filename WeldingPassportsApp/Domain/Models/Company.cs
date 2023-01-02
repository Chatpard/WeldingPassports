using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Company : IDomainModel
    {
        public int ID { get; set; }
        public int? AddressID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMainPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string WebPage { get; set; }

        // Navigation Properties
        public Address Address { get; set; }
        public ExamCenter ExamCenter { get; set; }
        public TrainingCenter TrainingCenter { get; set; }
        public ICollection<CompanyContact> CompanyContacts { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
