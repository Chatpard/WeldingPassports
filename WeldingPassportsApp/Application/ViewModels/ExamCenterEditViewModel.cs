using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ExamCenterEditViewModel
    {
        public string EncryptedID { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int? CompanyID { get; set; }

        public SelectList CompanySelectList { get; set; }

        [Display(Name = "Contact Name")]
        public int? CompanyContactID { get; set; }

        public SelectList CompanyContactSelectList { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
