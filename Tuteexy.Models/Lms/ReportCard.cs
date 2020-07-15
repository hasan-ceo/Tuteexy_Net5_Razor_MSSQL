using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tuteexy.Models
{
    public class ReportCard : EntryInfo
    {
        [Key]
        public long ReportCardID { get; set; }

        [Required]
        public long StudentID { get; set; }
        [Required]
        public string Subjects { get; set; }
        [Required]
        public float Term1Classmarks { get; set; }
        [Required]
        public float Term1ExamMarks { get; set; }
        [Required]
        public float Term1Total { get; set; }
        [Required]
        public string Term1Grade { get; set; }
        public string Term1RemarksCodeNo { get; set; }
        [Required]
        public float Term2Classmarks { get; set; }
        [Required]
        public float Term2ExamMarks { get; set; }
        [Required]
        public float Term2Total { get; set; }
        [Required]
        public string Term2Grade { get; set; }
        public string Term2RemarksCodeNo { get; set; }
        [Required]
        public float TotalMarksAverage { get; set; }
        [Required]
        public string AverageGrade { get; set; }
    }
}
