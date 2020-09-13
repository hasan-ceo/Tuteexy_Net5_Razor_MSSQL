using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsExamTmpSheet")]
    public class ExamTmpSheet
    {
        [Key]
        public long ExamTmpSheetID { get; set; }
        public long ExamTmpID { get; set; }
        [ForeignKey("ExamTmpID")]
        public virtual ExamTmp ExamTmp { get; set; }
        public string StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual ApplicationUser Student { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Date Assigned")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSubmitted { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(1024)]
        public string Description { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 1")]
        public string AttachLink1 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link  - 2")]
        public string AttachLink2 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link  - 3")]
        public string AttachLink3 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link  - 4")]
        public string AttachLink4 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 5")]
        public string AttachLink5 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 6")]
        public string AttachLink6 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 7")]
        public string AttachLink7 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 8")]
        public string AttachLink8 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 9")]
        public string AttachLink9 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 10")]
        public string AttachLink10 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 11")]
        public string AttachLink11 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 12")]
        public string AttachLink12 { get; set; }


        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 13")]
        public string AttachLink13 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 14")]
        public string AttachLink14 { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Attachment Link - 15")]
        public string AttachLink15 { get; set; }

        [DefaultValue(0)]
        [Display(Name = "ExamTmp marks")]
        public double ExmMarks { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        [Display(Name = "Status")]
        public string ExmStatus { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Comments")]
        public string ExmComments { get; set; }


    }
}
