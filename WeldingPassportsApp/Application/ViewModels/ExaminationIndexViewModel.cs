using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.ViewModels
{
    public class ExaminationIndexViewModel
    {
        [NotMapped]
        public string EncryptedID { get; set; }
        
        private DateTime? examDate;
        [DataType(DataType.Date)]
        [Display(Name = "Examination Date")]
        public DateTime? ExamDate
        {
            get
            {
                if (examDate != null)
                    return examDate.Value.Date;
                else
                    return null;
            }
            set
            {
                if (value != null)
                    examDate = value.Value.Date;
                else
                    examDate = null;
            }
        }
        
        [Display(Name = "Training Center")]
        public string CompanyNameTC { get; set; }

        [Display(Name = "Exam Center")]
        public string CompanyNameEC { get; set; }
        
        [Display(Name = "# Passports")]
        public int NumberOfPassports { get; set; }
    }
}
