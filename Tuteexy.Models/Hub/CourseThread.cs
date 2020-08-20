using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("HubCourseThread")]
    public class CourseThread
    {
        [Key]
        public long CourseThreadID { get; set; }

        public long CourseID { get; set; }
        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(1024)]
        [DefaultValue("")]
        public string Description { get; set; }

        [DefaultValue(false)]
        public bool IsReplyClose { get; set; }

        [DefaultValue(false)]
        public bool IsOffensive { get; set; }

        [DefaultValue(false)]
        public bool IsApproved { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Submitted Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittedDate { get; set; }

    }
}
