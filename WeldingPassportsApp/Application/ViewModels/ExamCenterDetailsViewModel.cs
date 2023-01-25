using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class ExamCenterDetailsViewModel
    {
        public string EncryptedID { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
