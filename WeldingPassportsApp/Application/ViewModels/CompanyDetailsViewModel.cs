using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class CompanyDetailsViewModel
    {
        public string EncryptedID { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Company Phone")]
        [RegularExpression(@"^(\s*((\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})|(\+\s*(\d\s*){2,3}(0\s*)?(\d\s*){8})|0\s*3\s*(\d\s*){7})\s*)$", ErrorMessage = "Incorrect phone number.")]
        public string CompanyMainPhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; }

        [Url]
        [Display(Name = "Web Page")]
        public string WebPage { get; set; }

        [Display(Name = "Address")]
        public string BusinessAddress { get; set; }

        [Display(Name = "Postalcode")]
        public string BusinessAddressPostalCode { get; set; }

        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }

        [Display(Name = "Country")]
        public string BusinessAddressCountry { get; set; }
    }
}
