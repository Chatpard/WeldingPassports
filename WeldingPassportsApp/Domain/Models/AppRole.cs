using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class AppRole: IdentityRole
    {
        [Column(TypeName = "varchar(64)")]
        public string RoleName { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}