using Application.Interfaces.Controllers;
using Application.SQLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Application.Utilities;

namespace Application.ViewModels
{
    public class ExamCenterCreateViewModel
    {
        [Required]
        [Display(Name="Company")]
        //Todo: Controller string replace with ref.
        [Remote(action: nameof(IExamCentersApiController.IsCompanyIDInUse), controller: "ExamCentersApi", areaName: "API", ErrorMessage = "This company is already in use.")]
        public int CompanyID { get; set; }

        public SelectList CompanySelectList { get; set; }

        public bool IsActive { get; set; }
    }
}
