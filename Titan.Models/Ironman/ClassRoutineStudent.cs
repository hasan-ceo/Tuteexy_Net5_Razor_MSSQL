using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class ClassRoutineStudent
    {
        [Key]
        public long ClassRoutineStudentID { get; set; }
        public long StudentID { get; set; }
        public string DayName { get; set; }
        public string Period1 { get; set; }
        public string Period2 { get; set; }
        public string Period3 { get; set; }
        public string Period4 { get; set; }
        public string Period5 { get; set; }
        public string Period6 { get; set; }
        public string Period7 { get; set; }
        public string Period8 { get; set; }
        public string Period9 { get; set; }
        public string Period10 { get; set; }
    }
}
