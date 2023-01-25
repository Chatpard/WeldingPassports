using Application.Interfaces.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Application.ViewModels
{
    public class ExamCenterEditViewModel
    {
        public string EncryptedID { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyID { get; set; }

        public SelectList CompanySelectList { get; set; }

        public bool IsActive { get; set; }
    }
}
