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
        public string AVNumber { get; set; }
        
        public int TrainingCenterID { get; set; }
        
        public int PEWelderID { get; set; }

        public SelectList TrainingCenterSelectList { get; set; }

        public SelectList PEWelderSelectList { get; set; }
    }
}
