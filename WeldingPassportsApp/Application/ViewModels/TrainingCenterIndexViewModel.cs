using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class TrainingCenterIndexViewModel
    {
        public string EncryptedId { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Company Letter")]
        public char Letter { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Postal Code")]
        public string BusinessAddressPostalCode { get; set; }

        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }

        [Display(Name = "Contact")]
        public string Contact { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string MobilePhone { get; set; }
    }
}
