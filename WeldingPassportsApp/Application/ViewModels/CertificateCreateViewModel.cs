using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class CertificateCreateViewModel
    {
        [Required]
        public string ExaminationEncryptedID { get; set; }

        private DateTime? examDate;
        [DataType(DataType.Date)]
        [Display(Name = "Examination Date")]
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

        [Display(Name = "Exam Center")]
        public string ExamCenterName { get; set; }

        [Display(Name = "Training Center")]
        public string TrainingCenterName { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }
        public SelectList CompanyNameItems { get; set; }

        [Required]
        [Display(Name = "PE Passport")]
        public int PEPassportID { get; set; }
        public SelectList PEPassportAVNumberItems { get; set; }

        [Required]
        [Display(Name = "Process")]
        public int ProcessID { get; set; }
        public SelectList ProcessNameItems { get; set; }

        [Required]
        [Display(Name = "Registration Type")]
        public int RegistrationTypeID { get; set; }
        public SelectList RegistrationTypeNameItems { get; set; }
    }
}
