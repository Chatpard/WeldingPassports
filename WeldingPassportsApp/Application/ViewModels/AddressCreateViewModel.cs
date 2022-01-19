using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class AddressCreateViewModel
    {
        [Display(Name = "Address")]
        public string BusinessAddress { get; set; }

        [Display(Name = "Postalcode")]
        public string BusinessAddressPostalCode { get; set; }

        [Display(Name = "City")]
        public string BusinessAddressCity { get; set; }

        [Display(Name = "Country")]
        public string BusinessAddressCountry { get; set; }
    }
}
