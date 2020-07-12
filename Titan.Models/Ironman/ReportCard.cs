using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class ReportCard
    {
        [Key]
        public long ReportCardID { get; set; }
        public long StudentID { get; set; }
        public string Subjects { get; set; }
        public float Term_1_Classmarks { get; set; }
        public float Term_1_ExamMarks { get; set; }
        public float Term_1_Total { get; set; }
        public string Term_1_Grade { get; set; }
        public string Term_1_RemarksCodeNo { get; set; }
        public float Term_2_Classmarks { get; set; }
        public float Term_2_ExamMarks { get; set; }
        public float Term_2_Total { get; set; }
        public float Term_2_Grade { get; set; }
        public string Term_2_RemarksCodeNo { get; set; }
        public float TotalMarksAverage { get; set; }
        public string AverageGrade { get; set; }
    }
}
