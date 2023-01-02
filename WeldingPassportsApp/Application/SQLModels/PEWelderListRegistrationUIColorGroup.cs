using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class PEWelderListRegistrationUIColorGroup
    {
        public PEWelder PEWelder { get; set; }
        public IEnumerable<PEPassportRegistrationUIColorGroup> PEPassportRegistrationUIColorGroupList { get; set; }
    }
}
