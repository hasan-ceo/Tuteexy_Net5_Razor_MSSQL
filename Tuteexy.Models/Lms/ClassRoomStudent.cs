using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsClassRoomStudent")]
    public class ClassRoomStudent

    {
        [Key]
        public long ClassRoomStudentID { get; set; }

        public long ClassRoomID { get; set; }
        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom { get; set; }

        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual ApplicationUser Student { get; set; }

        [MaxLength(50)]
        public string ApprovedBy { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Approved Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public DateTime ApprovedDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
