using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models
{
    public class ClassRoutineStudent : EntryInfo
    {
        [Key]
        public long ClassRoutineStudentID { get; set; }
        public long StudentID { get; set; }
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
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 4")]
        public string Period4 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 5")]
        public string Period5 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 6")]
        public string Period6 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 7")]
        public string Period7 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 8")]
        public string Period8 { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Period - 9")]
        public string Period9 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Period - 10")]
        public string Period10 { get; set; }
    }
}
