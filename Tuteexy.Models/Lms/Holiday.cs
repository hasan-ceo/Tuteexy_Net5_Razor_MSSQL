using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsHoliday")]
    public class Holiday : EntryInfo
    {
        [Key]
        public long HolidayID { get; set; }
        public long SchoolID { get; set; }
        [ForeignKey("SchoolID")]
        public School School { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Date Assigned")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Date Assigned")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Holiday Name")]
        [Required]
        [MaxLength(128)]
        public string HolidayName { get; set; }

        [Display(Name = "Duration of Holiday")]
        public int Duration { get; set; }
    }
}
