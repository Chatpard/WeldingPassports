using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class PEPassportRegistrationUIColorGroup
    {
        public PEPassport PEPassport { get; set; }
        public Registration Registration { get; set; }
        public UIColor UIColor { get; set; }
    }
}
