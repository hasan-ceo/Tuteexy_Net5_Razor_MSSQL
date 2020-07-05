using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Holiday
    {
        [Key]
        public int HolidayID { get; set; }

        [Display(Name="Holiday Name")]
        [Required]
        [MaxLength(50)]
        public string HolidayName { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
