using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsHomeworkSheet")]
    public class HomeworkSheet
    {
        [Key]
        public long HomeworkSheetID { get; set; }
        public long HomeworkID { get; set; }
        [ForeignKey("HomeworkID")]
        public virtual Homework Homework { get; set; }
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

        [DefaultValue(0)]
        [Display(Name = "Homework marks")]
        public double HwMarks { get; set; }

        [DefaultValue("")]
        [MaxLength(20)]
        [Display(Name = "Status")]
        public string HWStatus { get; set; }

        [DefaultValue("")]
        [MaxLength(150)]
        [Display(Name = "Comments")]
        public string HWComments { get; set; }


    }
}
