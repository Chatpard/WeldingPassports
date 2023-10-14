using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEWelderIndexViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public char? Letter { get; set; }

        [Display(Name = "AV Number")]
        [DisplayFormat(DataFormatString = "{0:D5}")]
        public int? AVNumber { get; set; }

        public string Color { get; set; }

        public string Tooltip { get; set; }
    }
}