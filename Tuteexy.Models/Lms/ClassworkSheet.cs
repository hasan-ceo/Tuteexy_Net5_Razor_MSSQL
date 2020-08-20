using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsClassworkSheet")]
    public class ClassworkSheet
    {
        [Key]
        public long ClassworkSheetID { get; set; }

        public long ClassworkID { get; set; }
        [ForeignKey("ClassworkID")]
        public virtual Classwork Classwork { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(1024)]
        [DefaultValue("")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Submitted Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittedDate { get; set; }

    }
}
