using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Application.ViewModels
{
    public class AddressEditViewModel : AddressCreateViewModel
    {
        public string EncryptedID { get; set; }
    }
}
