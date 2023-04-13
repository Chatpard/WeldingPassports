using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class CompanyContact : IDomainModel
    {
        
        public int ID { get; set; }

        public int ContactID { get; set; }

        public int? CompanyID { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }

        public int? AddressID { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string JobTitle { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string BusinessPhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string MobilePhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string Email { get; set; }

        // Navigation Properties
        public Contact Contact { get; set; }
        public Company Company { get; set; }
        public AppUser AppUser { get; set; }
        public Address Address { get; set; }
        public ICollection<TrainingCenter> TrainingCenters { get; set; }
        public ICollection<ExamCenter> ExamCenters { get; set; }
        public ICollection<Revoke> Revokes { get; set; }
    }
}

// Field length from https://www.manageengine.com/microsoft-365-management-reporting/help/management/csv-instructions.html
