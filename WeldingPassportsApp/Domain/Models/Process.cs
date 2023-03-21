using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Process : IDomainModel
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string ProcessName { get; set; }
        
        public bool IsActive { get; set; }

        // Navigation Properties
        public ICollection<Registration> Registrations { get; set; }
    }
}
