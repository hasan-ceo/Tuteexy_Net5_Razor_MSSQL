using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Titan.Models
{
    [Table("LmsClassRoomStudent")]
    public class ClassRoomStudent

    {
        [Key]
        public long ClassRoomStudentID { get; set; }

        public long ClassRoomID { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }

        public string StudentID { get; set; }
        public virtual ApplicationUser Student { get; set; }

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
