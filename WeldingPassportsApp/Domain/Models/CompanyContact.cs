using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class CompanyContact : IDomainModel
    {
        public int ID { get; set; }

        public int ContactID { get; set; }

        public int? CompanyID { get; set; }

        public string IdentityUserId { get; set; }

        public int? AddressID { get; set; }

        public string JobTitle { get; set; }

        public string BusinessPhone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

        // Navigation Properties
        public Contact Contact { get; set; }
        public Company Company { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public Address Address { get; set; }
        public ListExamCenter ListExamCenter { get; set; }
        public ListTrainingCenter ListTrainingCenter { get; set; }
        public ICollection<Revoke> Revokes { get; set; }
    }
}
