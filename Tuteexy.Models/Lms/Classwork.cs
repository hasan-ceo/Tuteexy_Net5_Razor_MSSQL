using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsClasswork")]
    public class Classwork
    {
        [Key]
        public long ClassworkID { get; set; }

        public long ClassRoomID { get; set; }
        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom { get; set; }

        public string TeacherID { get; set; }
        [ForeignKey("TeacherID")]
        public virtual ApplicationUser Teacher { get; set; }

        [Required(ErrorMessage = "Select Subject")]
        [MaxLength(64)]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(4000)]
        public string Description { get; set; }

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

        [MaxLength(150)]
        [Display(Name = "Ref Link - 1")]
        public string RefLink1 { get; set; }

        [MaxLength(150)]
        [Display(Name = "Ref Link  - 2")]
        public string RefLink2 { get; set; }

        [MaxLength(150)]
        [Display(Name = "Ref Link  - 3")]
        public string RefLink3 { get; set; }

        [MaxLength(150)]
        [Display(Name = "Ref Link  - 4")]
        public string RefLink4 { get; set; }

        [MaxLength(150)]
        [Display(Name = "Ref Link - 5")]
        public string RefLink5 { get; set; }

    }
}
