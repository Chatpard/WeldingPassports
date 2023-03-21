using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class AllowedRegistrationType : IDomainModel
    {
        public int ID { get; set; }

        [ForeignKey(nameof(AllowedRegistrationType.PreviousRegistrationType))]
        public int? PreviousRegistrationTypeID { get; set; }
        public RegistrationType PreviousRegistrationType { get; set; }


        [ForeignKey(nameof(AllowedRegistrationType.AvailableRegistrationType))]
        public int? AvailableRegistrationTypeID { get; set; }
        public RegistrationType AvailableRegistrationType { get; set; }


        public ExtendableStatus ExtendableStatus { get; set; }

        public bool? HasPassed { get; set; }

        public bool? IsNew { get; set; }

        [ForeignKey(nameof(AllowedRegistrationType.CurrentRegistrationType))]
        public int? CurrentRegistrationTypeID { get; set; }
        public RegistrationType CurrentRegistrationType { get; set; }

        // Navigation Properties

    }
}
// https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table