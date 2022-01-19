using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class CompanyContactDetailsViewModel
    {
        private DateTime? birthday;

        public string EncryptedID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

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

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Business Phone")]
        [RegularExpression(@"^(\s*((\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})|(\+\s*(\d\s*){2,3}(0\s*)?(\d\s*){8})|0\s*3\s*(\d\s*){7})\s*)$", ErrorMessage = "Incorrect phone number.")]
        public string BusinessPhone { get; set; }

        [Display(Name = "Mobile Phone")]
        [RegularExpression(@"^\s*(\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})\s*$", ErrorMessage = "Incorrect mobile phone number.")]
        public string MobilePhone { get; set; }

        [Display(Name = "Address")]
        public string BusinessAddress { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression(@"^(\s*\d){4}\s*$", ErrorMessage = "Incorrect postal code.")]
        public string BusinessAddressPostalCode { get; set; }

        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }

        [Display(Name = "Country")]
        public string BusinessAddressCountry { get; set; }
    }
}
