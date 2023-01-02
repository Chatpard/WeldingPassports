using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class CompanyContactIndexViewModel
    {
        public string EncryptedID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Company Phone")]
        public string BusinessPhone { get; set; }
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
    }
}
