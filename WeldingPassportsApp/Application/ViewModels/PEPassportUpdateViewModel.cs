using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Application.ViewModels
{
    public class PEPassportUpdateViewModel
    {
        public PEPassportUpdateViewModel()
        {
            Certifications = new List<PEPassportDetailsRegistrationsIndexViewModel>();
        }

        public string EncryptedID { get; set; }

        [Display(Name = "Letter")]
        public char Letter { get; set; }

        [Display(Name = "AV Number")]
        [DisplayFormat(DataFormatString = "{0:D5}")]
        public int AVNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "National Number")]
        public string NationalNumber { get; set; }

        [Display(Name = "ID Number")]
        public string IdNumber { get; set; }

        public List<PEPassportDetailsRegistrationsIndexViewModel> Certifications { get; set; }
    }
}
