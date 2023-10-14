using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEPassportDetailsRegistrationsIndexViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }
        
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
        private DateTime? examDate;
        
        [DataType(DataType.Date)]
        [Display(Name = "Exam Date")]
        public DateTime? ExamDate
        {
            get
            {
                if (examDate != null)
                    return examDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    examDate = value.Value.Date;
                else
                    examDate = null;
            }
        }
        
        [Display(Name = "Company")]
        public string CompanyName { get; set; }
        
        [Display(Name = "Process")]
        public string ProcessName { get; set; }
        
        [Display(Name = "Registration Type")]
        public string RegistrationTypeName { get; set; }
        
        [UIHint("HasPassed")]
        [Display(Name = "Passed")]
        public bool? HasPassed { get; set; }
        
        public bool IsRevoked { get; set; }
        
        public string Color { get; set; }

        public string Tooltip { get; set; }
        
        public bool HasNextOrRevoked { get; set; }
    }
}
