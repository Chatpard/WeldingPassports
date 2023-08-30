using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Company : IDomainModel
    {
        public int ID { get; set; }

        public int? AddressID { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyMainPhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyEmail { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string WebPage { get; set; }

        // Navigation Properties
        public Address Address { get; set; }
        public ExamCenter ExamCenter { get; set; }
        public TrainingCenter TrainingCenter { get; set; } 
        public ICollection<CompanyContact> CompanyContacts { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}

// Field length from https://www.manageengine.com/microsoft-365-management-reporting/help/management/csv-instructions.html