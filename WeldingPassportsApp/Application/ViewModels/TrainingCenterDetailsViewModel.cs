using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class TrainingCenterDetailsViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }
        [Display(Name = "Company Letter")]
        public char Letter { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [Display(Name = "Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Phone")]
        public string CompanyMainPhone { get; set; }
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; }
        [Display(Name = "Web Page")]
        public string WebPage { get; set; }
        // Training Center Address
        [Display(Name = "Address")]
        public string TrainingCenterAddress { get; set; }
        [Display(Name = "City")]
        public string TrainingCenterAddressCity { get; set; }
        [Display(Name = "Postal Code")]
        public string TrainingCenterAddressPostalCode { get; set; }
        [Display(Name = "Country")]
        public string TrainingCenterAddressCountry { get; set; }
        // Training Center Contact Information
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        private DateTime? birthday;
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
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Company Phone")]
        public string BusinessPhone { get; set; }
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
        // Business Address
        [Display(Name = "Address")]
        public string BusinessAddress { get; set; }
        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }
        [Display(Name = "Postal Code")]
        public string BusinessAddressPostalCode { get; set; }
        [Display(Name = "Country")]
        public string BusinessAddressCountry { get; set; }
    }
}
