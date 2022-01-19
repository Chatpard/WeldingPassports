﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ContactCreateViewModel
    {
        private DateTime? birthday;

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

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
        //public ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
