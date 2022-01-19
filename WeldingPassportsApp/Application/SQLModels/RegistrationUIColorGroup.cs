using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.SQLModels
{
    public class RegistrationUIColorGroup
    {
        public Registration Registration { get; set; }
        public UIColor UIColor { get; set; }
    }
}
