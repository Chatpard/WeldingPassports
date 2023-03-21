using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class UserToApprove : UserToApproveIndex
    {
        private DateTime? birthday;
        public DateTime? Birthday
        {
            get
            {
                if (birthday != null)
                    return birthday.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    birthday = value.Value.Date;
                else
                    birthday = null;
            }
        }

        [Column(TypeName = "varchar(64)")]
        public string JobTitle { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string MobilePhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string BusinessPhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "varchar(1024)")]
        public string BusinessAddress { get; set; }

        [Column(TypeName = "varchar(40)")]
        public string BusinessAddressPostalCode { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string BusinessAddressCity { get; set; }

        [Column(TypeName = "varchar(128)")]
        public string BusinessAddressCountry { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyMainPhone { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string CompanyEmail { get; set; }

        [Column(TypeName = "varchar(2048)")]
        public string WebPage { get; set; }

        [Column(TypeName = "varchar(64)")]
        public bool EmailConfirmed { get; set; }

        [Column(TypeName = "varchar(64)")]
        public string EmailLanguage { get; set; }
    }
}

// Field lengths from https://www.manageengine.com/microsoft-365-management-reporting/help/management/csv-instructions.html
