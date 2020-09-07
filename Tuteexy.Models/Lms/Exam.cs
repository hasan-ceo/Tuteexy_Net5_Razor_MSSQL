using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsExam")]
    public class Exam
    {
        [Key]
        public long ExamID { get; set; }

        public long ClassRoomID { get; set; }
        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom { get; set; }

        public string TeacherID { get; set; }
        [ForeignKey("TeacherID")]
        public virtual ApplicationUser Teacher { get; set; }

        [MaxLength(150)]
        public string TeacherName { get; set; }

        [Required(ErrorMessage = "Select Subject")]
        [MaxLength(64)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Date Assigned")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAssigned { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Class Start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TimeStart { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Class End")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TimeEnd { get; set; }

    }
}
