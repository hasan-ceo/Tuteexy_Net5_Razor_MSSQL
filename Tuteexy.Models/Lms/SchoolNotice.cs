using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsSchoolNotice")]
    public class SchoolNotice : EntryInfo
    {
    [Key]
        public long SchoolNoticeID { get; set; }
        public long SchoolID { get; set; }
        public virtual School School { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [Required]
        [MaxLength(4000)] 
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Schedule Due")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ScheduleDateTime { get; set; }

    }
}
