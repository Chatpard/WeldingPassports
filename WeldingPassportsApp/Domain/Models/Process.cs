using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Process : IDomainModel
    {
        public int ID { get; set; }
        public string ProcessName { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public ICollection<Registration> Registrations { get; set; }
    }
}
