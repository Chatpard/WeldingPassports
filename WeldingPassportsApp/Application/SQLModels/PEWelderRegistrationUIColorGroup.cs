using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SQLModels
{
    public class PEWelderRegistrationUIColorGroup
    {
        public PEWelder PEWelder { get; set; }
        public Registration Registration { get; set; }
        public UIColor UIColor { get; set; }
    }
}
