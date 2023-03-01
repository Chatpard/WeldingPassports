using System;
using System.Collections.Generic;
using System.Text;

namespace Application.APIModels
{
    public class ExpiryDateAPIModel
    {
        public string? ExpiryDate { get; set; }

        public string MinExpiryDate { get; set; }
        
        public string MaxExpiryDate { get; set;}

        public string ExpiryDateErrorMessage { get; set; }

        public string RevokeDateErrorMessage { get; set; }
    }
}
