using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class ExaminationDetailsCertificationsIndexViewModel
    {
        public string EncryptedID { get; set; }

        public char Letter { get; set; }

        [Display(Name = "AV Number")]
        [DisplayFormat(DataFormatString = "{0:D5}")]
        public int AVNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Process")] 
        public string ProcessName { get; set; }

        [Display(Name = "Registration Type")] 
        public string RegistrationTypeName { get; set; }

        [Display(Name = "Passed")] 
        public bool? HasPassed { get; set; }

        public bool IsRevoked { get; set; }

        public string Color { get; set; }

    }
}
