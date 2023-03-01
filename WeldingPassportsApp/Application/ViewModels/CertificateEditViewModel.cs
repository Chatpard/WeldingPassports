using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class CertificateEditViewModel
    {
        [Required]
        public string EncryptedID { get; set; }

        public int PEPassportID { get; set; }

        public char Letter { get; set; }

        [Display(Name = "AV Number")]
        public int AVNumber { get; set; }

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
        public int CompanyID { get; set; }
        public SelectList CompanyNameItems { get; set; }

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

        private DateTime? maxExpiryDate;
        public DateTime? MaxExpiryDate
        {
            get
            {
                if (maxExpiryDate != null)
                    return maxExpiryDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    maxExpiryDate = value.Value.Date;
                else
                    maxExpiryDate = null;
            }
        }

        [Display(Name = "Registration Type")]
        public int RegistrationTypeID { get; set; }
        public SelectList RegistrationTypeNameItems { get; set; }

        [Display(Name = "Passed")]
        public bool? HasPassed { get; set; }

        [Display(Name = "Exam Center")]
        public string ExamCenterName { get; set; }

        public bool HasNext { get; set; }

        [Display(Name = "Revoked By")]
        public int? RevokedByCompanyContactID { get; set; }
        public SelectList CompanyContactNameItems { get; set; }

        private DateTime? revokeDate;
        [DataType(DataType.Date)]
        [Display(Name = "Revoke Date")]
        public DateTime? RevokeDate
        {
            get
            {
                if (revokeDate != null)
                    return revokeDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    revokeDate = value.Value.Date;
                else
                    revokeDate = null;
            }
        }

        [Display(Name = "Revoke Comment")]
        public string RevokeComment { get; set; }

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
