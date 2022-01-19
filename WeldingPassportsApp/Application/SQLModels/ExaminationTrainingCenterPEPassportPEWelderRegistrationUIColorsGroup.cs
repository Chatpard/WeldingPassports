using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup
    {
        public Examination Examination { get; set; }
        public IEnumerable<RegistrationUIColorGroup> PEPassportPEWelderRegistrationUIColors { get; set; }
    }
}
