using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class RegistrationType : IDomainModel
    {
        public int ID { get; set; }
        public string RegistrationTypeName { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        [InverseProperty(nameof(AllowedRegistrationType.RegistrationType))]
        public ICollection<AllowedRegistrationType> AvailableRegistrationTypes { get; set; }
        [InverseProperty(nameof(AllowedRegistrationType.AvailableRegistrationType))]
        public ICollection<AllowedRegistrationType> PreviousRegistrationTypes { get; set; }
    }
}
// https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table