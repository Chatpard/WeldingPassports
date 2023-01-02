using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class ExaminationUpdateViewModel
    {
        public ExaminationUpdateViewModel()
        {
            Certifications = new List<ExaminationDetailsCertificationsIndexViewModel>();
        }

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
        
        [Display(Name = "Exam Center")]
        public string ExamCenterName { get; set; }
        
        [Display(Name = "Training Center")]
        public string TrainingCenterName { get; set; }
        
        public List<ExaminationDetailsCertificationsIndexViewModel> Certifications { get; set; }
    }
}
