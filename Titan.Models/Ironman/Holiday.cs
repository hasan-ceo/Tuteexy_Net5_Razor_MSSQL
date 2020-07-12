using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Holiday
    {
        [Key]
        public long HolidayID { get; set; }
        public long SchoolID { get; set; }
        //public virtual School School { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        [Display(Name = "Holiday Name")]
        [Required]
        [MaxLength(100)]
        public string NameHoliday { get; set; }
        public int DurationHoliday { get; set; }
    }
}
