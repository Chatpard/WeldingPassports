using Application.SQLModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    [SaveTempData]
    public class CompanyContactEditViewModel
    {
        public CompanyContactEditViewModel()
        {
            ContactSelectList = new SelectList(new List<ContactSelectListSQLModel>());
            CompanySelectList = new SelectList(new List<CompanySelectListSQLModel>());
            AddressSelectList = new SelectList(new List<AddressSelectListSQLModel>());
            AppUsersSelectList = new SelectList(new List<AppUsersSelectListSQLModel>());
            RoleNamesSelectList = new SelectList(new List<RoleNamesSelectListSQLModel>());
        }

        public string EncryptedID { get; set; }

        [TempData]
        [Display(Name = "Contact Name")]
        public int ContactID { get; set; }

        [JsonIgnore]
        public SelectList ContactSelectList { get; set; }

        [Display(Name = "User")]
        public string AppUserID { get; set; }

        [JsonIgnore]
        public SelectList AppUsersSelectList { get; set; }

        [Display(Name = "Role")]
        public string AppRoleID { get; set; }

        [JsonIgnore]
        public SelectList RoleNamesSelectList { get; set; }

        [Display(Name = "Company Name")]
        public int? CompanyID { get; set; }

        [JsonIgnore]
        public SelectList CompanySelectList { get; set; }

        [Display(Name = "Address")]
        public int? AddressID { get; set; }

        [JsonIgnore]
        public SelectList AddressSelectList { get; set; }

        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Display(Name = "Business Phone")]
        [RegularExpression(@"^(\s*((\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})|(\+\s*(\d\s*){2,3}(0\s*)?(\d\s*){8})|0\s*3\s*(\d\s*){7})\s*)$", ErrorMessage = "Incorrect phone number.")]
        public string BusinessPhone { get; set; }

        [Display(Name = "Mobile Phone")]
        [RegularExpression(@"^\s*(\+\s*(\d\s*){11}|0\s*4\s*(\d\s*){8})\s*$", ErrorMessage = "Incorrect mobile phone number.")]
        public string MobilePhone { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}