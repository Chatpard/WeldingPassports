using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class AllowedRegistrationType : IDomainModel
    {
        public int ID { get; set; }
        [ForeignKey(nameof(RegistrationType)), Column(Order = 0)]
        public int? RegistrationTypeID { get; set; }
        [ForeignKey(nameof(AvailableRegistrationType)), Column(Order = 1)]
        public int AvailableRegistrationTypeID { get; set; }
        public ExtendableStatus ExtendableStatus { get; set; }
        public bool? HasPassed { get; set; }

        // Navigation Properties
        public RegistrationType RegistrationType { get; set; }
        public RegistrationType AvailableRegistrationType { get; set; }
    }
}
// https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table