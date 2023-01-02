using Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Localization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ViewModels
{
    public class UserRegistrationViewModel : UserToApproveIndexViewModel
    {
        private DateTime? birthday;
        [Required]
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
        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Required]
        [RegularExpression(@"^\s*(\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})\s*$", ErrorMessage = "Incorrect mobile phone number.")]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
        [Required]
        [RegularExpression(@"^(\s*((\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})|(\+\s*(\d\s*){2,3}(0\s*)?(\d\s*){8})|0\s*3\s*(\d\s*){7})\s*)$",
            ErrorMessage = "Incorrect phone number.")]
        [Display(Name = "Business Phone")]
        public string BusinessPhone { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CompanyName { get; set; }
        public List<SelectListItem> CompanyNameItems { get; set; } = new List<SelectListItem>();
        [Required]
        [Display(Name = "Address")]
        public string BusinessAddress { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^(\s*\d){4}\s*$", ErrorMessage = "Incorrect postal code.")]
        public string BusinessAddressPostalCode { get; set; }
        [Required]
        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string BusinessAddressCountry { get; set; }
        [Required]
        [RegularExpression(@"^(\s*((\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})|(\+\s*(\d\s*){2,3}(0\s*)?(\d\s*){8})|0\s*3\s*(\d\s*){7})\s*)$",
            ErrorMessage = "Incorrect phone number.")]
        [Display(Name = "Phone")]
        public string CompanyMainPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; }
        [Required]
        [Display(Name = "Web Page")]
        public string WebPage { get; set; }
    }
}
