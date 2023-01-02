using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UIColor : IDomainModel
    {
        public int ID { get; set; }
        public ExtendableStatus ExtendableStatus { get; set; }
        public bool? HasPassed { get; set; }
        public string Color { get; set; }
    }
}
