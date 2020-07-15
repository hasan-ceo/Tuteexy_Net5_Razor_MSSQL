using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsSchoolTeacher")]
    public class SchoolTeacher

    {
        [Key]
        public long SchoolTeacherID { get; set; }

        public long SchoolID { get; set; }
        public virtual School School { get; set; }

        
        public string TeacherID { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        [MaxLength(50)]
        public string ApprovedBy { get; set; }

        [Required]
        [Display(Name = "Approved Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime ApprovedDate { get; set; }

        public bool IsApproved { get; set; }
    }
}
