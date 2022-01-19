using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class CompanyIndexViewModel
    {
        public string EncryptedID { get; set; }
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        [Display(Name = "Phone")]
        public string CompanyMainPhone { get; set; }
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; }
        [Display(Name = "Web Page")]
        public string WebPage { get; set; }
    }
}
