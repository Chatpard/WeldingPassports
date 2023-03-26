using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class AppUser: IdentityUser
    {
        public virtual CompanyContact CompanyContact { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}