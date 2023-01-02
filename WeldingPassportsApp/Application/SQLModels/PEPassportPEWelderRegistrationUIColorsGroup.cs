using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class PEPassportPEWelderRegistrationUIColorsGroup
    {
        public PEPassport PEPassport { get; set; }
        public PEWelder PEWelder { get; set; }
        public string SelectedCertificationEncryptedID { get; set; }
        public IEnumerable<RegistrationUIColorGroup> RegistrationUIColors { get; set; }
    }
}
