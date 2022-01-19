using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEWelderCreateViewModel
    {
        public string EncryptedID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "National Number")]
        public string NationalNumber { get; set; }
        
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; }
    }
}
