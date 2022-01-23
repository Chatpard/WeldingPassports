using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEWelderEditViewModel
    {
        [Required]
        public string EncryptedID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression(@"^[0-9]{2}[.\- ]{0,1}[0-9]{2}[.\- ]{0,1}[0-9]{2}[.\- ]{0,1}[0-9]{3}[.\- ]{0,1}[0-9]{2}$",
            ErrorMessage = "Incorrect National Number.")]
        [Display(Name = "National Number")]
        public string NationalNumber { get; set; }

        [RegularExpression(@"^[0-9]{3}[.\- ]{0,1}[0-9]{7}[.\- ]{0,1}[0-9]{2}$",
            ErrorMessage = "Incorrect ID Number.")]
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; }
    }
}
