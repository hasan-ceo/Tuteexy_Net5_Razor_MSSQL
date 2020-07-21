using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsClassRoutine")]
    public class ClassRoutine : EntryInfo
    {
        [Key]
        public long ClassRoutineID { get; set; }
        public long ClassRoomID { get; set; }
        public virtual ClassRoom ClassRoom {get; set;}

        [Required]
        [MaxLength(50)]
        public string DayName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 1")]
        public string Period1 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 2")]
        public string Period2 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 3")]
        public string Period3 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 4")]
        public string Period4 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 5")]
        public string Period5 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 6")]
        public string Period6 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 7")]
        public string Period7 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 8")]
        public string Period8 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 9")]
        public string Period9 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Period - 10")]
        public string Period10 { get; set; }
    }
}
