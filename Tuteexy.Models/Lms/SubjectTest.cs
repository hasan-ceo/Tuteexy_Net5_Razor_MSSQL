using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("Subjecttest",Schema ="Txt")]
    public class Subjecttest : EntryInfo

    {
        [Key]
        public long SubjectID { get; set; }
        public long SchoolID { get; set; }
        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }

        [Display(Name = "Subject")]
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }


    }
}
