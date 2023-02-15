using Application.Interfaces.Controllers;
using Application.Interfaces.Repositories.API;
using Application.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class PEPassportCreateViewModel
    {
        public char Letter { get; set; }

        [Display(Name = "AV Number")]
        [Remote(action: "GetIsAVNumberAvailable", controller: "PEPassportsApi", AdditionalFields = "Letter", ErrorMessage = "This AV number is already in use")]
        public string AVNumber { get; set; }
        
        [Display(Name = "Training Center")]
        public int TrainingCenterID { get; set; }

        [Display(Name = "Welder")]
        public int PEWelderID { get; set; }

        public SelectList TrainingCenterSelectList { get; set; }

        public SelectList PEWelderSelectList { get; set; }
    }
}
