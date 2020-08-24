using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsClassworkAttendance")]
    public class ClassworkAttendance
    {
        [Key]
        public long ClassworkAttendanceID { get; set; }

        public long ClassworkID { get; set; }
        [ForeignKey("ClassworkID")]
        public virtual Classwork Classwork { get; set; }

        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual ApplicationUser Student { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        [Display(Name = "Attendance Status")]
        public string AttenStatus { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Work Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime WorkDate { get; set; }

    }
}
