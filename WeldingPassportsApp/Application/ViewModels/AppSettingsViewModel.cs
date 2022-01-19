using Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class AppSettingsViewModel
    {
        public int ID { get; set; }
        [Required]
        [Range(1, 999)]
        [PropertyGreaterThan(new string[] { "MaxExtensionDays", "MaxInAdvanceDays" })]
        [Display(Name = "Maximum days after expiry for new expiry date")]
        public int MaxExpiryDays { get; set; }
        [Required]
        [Range(1, 999)]
        [PropertyLessThan(compareProperty: "MaxExpiryDays")]
        [Display(Name = "Maximum days after expiry for extension request")]
        public int MaxExtensionDays { get; set; }
        [Required]
        [Range(1, 999)]
        [PropertyLessThan(compareProperty: "MaxExpiryDays")]
        [Display(Name = "Maximum days in advance of expiry for extension request")]
        public int MaxInAdvanceDays { get; set; }
    }
}
