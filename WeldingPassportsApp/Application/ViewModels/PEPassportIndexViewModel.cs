using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEPassportIndexViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }

        public char Letter { get; set; }

        [Display(Name = "AV Number")]
        [DisplayFormat(DataFormatString = "{0:D5}")]
        public int AVNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        private DateTime? expiryDate;
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate
        {
            get
            {
                if (expiryDate != null)
                    return expiryDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    expiryDate = value.Value.Date;
                else
                    expiryDate = null;
            }
        }
        public string Color { get; set; }

        public string Tooltip { get; set; }
    }
}
