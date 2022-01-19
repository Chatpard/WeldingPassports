using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
    public class ExaminationCreateViewModel
    {
        private DateTime? examDate;
        [Required]
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
        [Required]
        [Display(Name = "Exam Center")]
        public string ExamCenterID { get; set; }
        public SelectList ExamCenterItems { get; set; }
        [Required]
        [Display(Name = "Training Center")]
        public string TrainingCenterID { get; set; }
        public SelectList TrainingCenterItems { get; set; }
    }
}
