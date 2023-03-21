using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class RegistrationType : IDomainModel
    {
        public int ID { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string RegistrationTypeName { get; set; }
        public bool IsActive { get; set; }

        public int? CurrentRegistrationTypeID { get; set; }

        public int? AllowedRegistrationTypeID { get; set; }

        // Navigation Properties
        [InverseProperty(nameof(AllowedRegistrationType.CurrentRegistrationType))]
        public ICollection<AllowedRegistrationType> CurrentRegistrationTypes { get; set; }

        [InverseProperty(nameof(AllowedRegistrationType.AvailableRegistrationType))]
        public ICollection<AllowedRegistrationType> AvailableRegistrationTypes { get; set; }

        [InverseProperty(nameof(AllowedRegistrationType.PreviousRegistrationType))]
        public ICollection<AllowedRegistrationType> PreviousRegistrationTypes { get; set; }
        //public ICollection<AllowedRegistrationType> PreviousRegistrationTypes { get; set; }
    }
}
// https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table