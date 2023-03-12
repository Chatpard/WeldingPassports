using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.APIModels
{
    public class GetProcessNamesReponse
    {
        public SelectList ProcessNamesSelectList { get; set; }
    }
}
