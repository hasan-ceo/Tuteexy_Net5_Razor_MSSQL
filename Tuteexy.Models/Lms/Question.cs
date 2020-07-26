using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsQuestion")]
    public class Question : EntryInfo
    {
        [Key]
        public long QuestionsID { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        public bool IsReplyClose { get; set; }
    }
}
