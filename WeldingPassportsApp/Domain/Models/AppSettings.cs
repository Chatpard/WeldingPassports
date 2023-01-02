using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AppSettings : IDomainModel
    {
        public int ID { get; set; }
        public int MaxExpiryDays { get; set; }
        public int MaxExtensionDays { get; set; }
        public int MaxInAdvanceDays { get; set; }
    }
}
