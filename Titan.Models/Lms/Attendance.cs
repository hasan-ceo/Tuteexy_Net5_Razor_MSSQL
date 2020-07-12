using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Attendance
    {
        [Key]
        public long Student_ID { get; set; }
        public string TypeAttendance { get; set; }
        public int Term1WorkingDays { get; set; }
        public int Term1Present { get; set; }
        public int Term1Absent { get; set; }
        public int Term1Late { get; set; }
        public int Term2WorkingDays { get; set; }
        public int Term2Present { get; set; }
        public int Term2Absent { get; set; }
        public int Term2Late { get; set; }
    }
}
