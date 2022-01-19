using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class CertificateEditViewModel
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
        public SelectList TrainingCenterNameItems { get; set; }

        [Display(Name = "Process")]
        public int ProcessID { get; set; }
        public SelectList ProcessNameItems { get; set; }


        // Current Certificate
        [Display(Name = "Company")]
        public int CurrentCertificateCompanyID { get; set; }
        public SelectList CompanyNameItems { get; set; }

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
        public int CurrentCertificateRegistrationTypeID { get; set; }
        public SelectList RegistrationTypeNameItems { get; set; }

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
        public int? CurrentCertificateRevokedByCompanyContactID { get; set; }
        public SelectList CompanyContactNameItems { get; set; }

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
