using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tuteexy.Models
{
    public class Holiday : EntryInfo
    {
        [Key]
        public long HolidayID { get; set; }
        public long SchoolID { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        [Display(Name = "Holiday Name")]
        [Required]
        [MaxLength(128)]
        public string NameHoliday { get; set; }

        [Display(Name = "Duration of Holiday")]
        public int DurationHoliday { get; set; }
    }
}
