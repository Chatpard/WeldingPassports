using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class TrainingCenterEditViewModel
    {
        public string EncryptedID { get; set; }
        
        [Display(Name = "Company Letter")]
        public char Letter { get; set; }

        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        public SelectList CompanySelectList { get; set; }

        [Display(Name = "Contact Name")]
        public int? CompanyContactID { get; set; }

        public SelectList CompanyContactSelectList { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
