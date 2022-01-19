using System;
using System.Collections.Generic;
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
        public string JobTitle { get; set; }
        public string MobilePhone { get; set; }
        public string BusinessPhone { get; set; }
        public string CompanyName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessAddressPostalCode { get; set; }
        public string BusinessAddressCity { get; set; }
        public string BusinessAddressCountry { get; set; }
        public string CompanyMainPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string WebPage { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailLanguage { get; set; }
    }
}
