using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsClassRoomNotice")]
    public class ClassRoomNotice : EntryInfo
    {
        [Key]
        public long ClassRoomNoticeID { get; set; }
        public long ClassRoomID { get; set; }
        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom { get; set; }

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
