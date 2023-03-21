using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Contact : IDomainModel
    {
        private DateTime? birthday;

        public int ID { get; set; }

        [Column(TypeName = "varchar(64)")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(64)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? Birthday
        {
            get 
            {
                if (birthday != null)
                    return birthday.Value.Date;
                else
                    return null;
            }
            set 
            {
                if (value != null)
                    birthday = value.Value.Date;
                else
                    birthday = null;
            }
        }

        // Navigation Properties
        public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}

// Field length from https://www.manageengine.com/microsoft-365-management-reporting/help/management/csv-instructions.html