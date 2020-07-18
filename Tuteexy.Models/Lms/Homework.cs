using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsHomework")]
    public class Homework 
    {
        [Key]
        public long HomeworkID { get; set; }

        public long ClassRoomID { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }

        public string TeacherID { get; set; }
        public virtual ApplicationUser Teacher { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "Homework Subject")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Homework Title")]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(4096)]
        public string Description { get; set; }

        ////Teacher who has assigned a homework
        //[Required]
        //[MaxLength(128)]
        //[Display(Name = "Homework assigned by:")]
        //public string Assignedby { get; set; }

        [Required]
        [Display(Name = "Date Assigned")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateAssigned { get; set; }

        [Required]
        [Display(Name = "Schedule Due")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ScheduleDate { get; set; }

        [Required]
        [Display(Name = "Date Due")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }

        [MaxLength(150)]
        [Display(Name = "Ref Link")]
        public string RefLink { get; set; }

        //[MaxLength(150)]
        //[Display(Name = "Attachment")]
        //public string Attachment { get; set; }

    }
}
