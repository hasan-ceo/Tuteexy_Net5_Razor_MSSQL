using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsClassRoutine")]
    public class ClassRoutine : EntryInfo
    {
        [Key]
        public long ClassRoutineID { get; set; }


        public long ClassRoomID { get; set; }
        [ForeignKey("ClassRoomID")]
        public virtual ClassRoom ClassRoom {get; set;}

        [Required(ErrorMessage = "Select Day")]
        [MaxLength(50)]
        public string DayName { get; set; }

        [Required(ErrorMessage ="Select Period - 1 Subject")]
        [MaxLength(50)]
        [Display(Name = "Period - 1")]
        public string Period1 { get; set; }
        [Required(ErrorMessage = "Select Period - 2 Subject")]
        [MaxLength(50)]
        [Display(Name = "Period - 2")]
        public string Period2 { get; set; }
        [Required(ErrorMessage = "Select Period - 3 Subject")]
        [MaxLength(50)]
        [Display(Name = "Period - 3")]
        public string Period3 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 4")]
        public string Period4 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 5")]
        public string Period5 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 6")]
        public string Period6 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 7")]
        public string Period7 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 8")]
        public string Period8 { get; set; }
       
        [MaxLength(50)]
        [Display(Name = "Period - 9")]
        public string Period9 { get; set; }
        [MaxLength(50)]
        [Display(Name = "Period - 10")]
        public string Period10 { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 1")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime1 { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 2")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime2 { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 3")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime3 { get; set; }

      
        [Display(Name = "Time - 4")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime4 { get; set; }


        [Display(Name = "Time - 5")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime5 { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 6")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime6 { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 7")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime7 { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 8")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime8 { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 9")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime9 { get; set; }

        [Column(TypeName = "datetime")]
        [Display(Name = "Time - 10")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        public DateTime PeriodTime10 { get; set; }
    }
}
