using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class PEPassportRegistrationGroup
    {
        public PEPassport PEPassport { get; set; }
        public Registration Registration { get; set; }
    }
}
