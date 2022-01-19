using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEWelderDetailsPEPassportsIndexViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }
        [Display(Name = "AV Number")]
        public string AVNumber { get; set; }
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
    }
}
