using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class CertificateDetailsViewModel
    {
        public string EncryptedID { get; set; }

        [Required]
        [Display(Name = "AV Number")]
        public string AVNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Training Center")]
        public string TrainingCenterName { get; set; }

        [Display(Name = "Process")]
        public string ProcessName { get; set; }


        // Current Certificate
        [Display(Name = "Company")]
        public string CurrentCertificateCompanyName { get; set; }
        
        private DateTime? currentCertificateExamDate;
        [DataType(DataType.Date)]
        [Display(Name = "Examination Date")]
        public DateTime? CurrentCertificateExamDate
        {
            get
            {
                if (currentCertificateExamDate != null)
                    return currentCertificateExamDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    currentCertificateExamDate = value.Value.Date;
                else
                    currentCertificateExamDate = null;
            }
        }
        
        private DateTime? currentCertificateExpiryDate;
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime? CurrentCertificateExpiryDate
        {
            get
            {
                if (currentCertificateExpiryDate != null)
                    return currentCertificateExpiryDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    currentCertificateExpiryDate = value.Value.Date;
                else
                    currentCertificateExpiryDate = null;
            }
        }

        [Display(Name = "Registration Type")]
        public string CurrentCertificateRegistrationTypeName { get; set; }

        [Display(Name = "Passed")]
        public bool? CurrentCertificateHasPassed { get; set; }

        [Display(Name = "Exam Center")]
        public string CurrentCertificateExamCenterName { get; set; }

        private DateTime? currentCertificateRevokeDate;
        [DataType(DataType.Date)]
        [Display(Name = "Revoke Date")]
        public DateTime? CurrentCertificateRevokeDate
        {
            get
            {
                if (currentCertificateRevokeDate != null)
                    return currentCertificateRevokeDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    currentCertificateRevokeDate = value.Value.Date;
                else
                    currentCertificateRevokeDate = null;
            }
        }

        [Display(Name = "Revoked By")]
        public string CurrentCertificateRevokedBy { get; set; }

        [Display(Name = "Revoke Comment")]
        public string CurrentCertificateRevokeComment { get; set; }


        // Previous Certificate
        private DateTime? previousCertificateExpiryDate;
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime? PreviousCertificateExpiryDate
        {
            get
            {
                if (previousCertificateExpiryDate != null)
                    return previousCertificateExpiryDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    previousCertificateExpiryDate = value.Value.Date;
                else
                    previousCertificateExpiryDate = null;
            }
        }

        [Display(Name = "Passed")]
        public bool? PreviousCertificateHasPassed { get; set; }

        private DateTime? previousCertificateRevokeDate;
        [DataType(DataType.Date)]
        [Display(Name = "Revoke Date")]
        public DateTime? PreviousCertificateRevokeDate
        {
            get
            {
                if (previousCertificateRevokeDate != null)
                    return previousCertificateRevokeDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    previousCertificateRevokeDate = value.Value.Date;
                else
                    previousCertificateRevokeDate = null;
            }
        }

        [Display(Name = "Revoked By")]
        public string PreviousCertificateRevokedBy { get; set; }

        [Display(Name = "Revoke Comment")]
        public string PreviousCertificateRevokeComment { get; set; }
    }
}
