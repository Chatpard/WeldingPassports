using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppUserRole: IdentityUserRole<string>
    {
        public virtual AppUser AppUser { get; set; }
        public virtual AppRole AppRole { get; set; }
    }
}
