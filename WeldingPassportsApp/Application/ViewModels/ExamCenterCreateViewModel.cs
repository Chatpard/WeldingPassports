using Application.Interfaces.Controllers;
using Application.SQLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ExamCenterCreateViewModel
    {
        public ExamCenterCreateViewModel()
        {
            CompanySelectList = new SelectList(new List<CompanySelectListSQLModel>());
            CompanyContactSelectList = new SelectList(new List<CompanyContactSelectListSQLModel>());
        }

        [Required]
        [Display(Name="Company")]
        //Todo: Controller string replace with ref.
        [Remote(action: nameof(IExamCentersApiController.IsCompanyIDInUse), controller: "ExamCentersApi", areaName: "API", ErrorMessage = "This company is already in use.")]
        public int CompanyID { get; set; }

        public SelectList CompanySelectList { get; set; }

        [Display(Name = "Contact Name")]
        public int? CompanyContactID { get; set; }

        public SelectList CompanyContactSelectList { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
