using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class UserToApproveViewModel : UserRegistrationViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        public List<SelectListItem> RoleItems { get; set; } = new List<SelectListItem>();
    }
}
