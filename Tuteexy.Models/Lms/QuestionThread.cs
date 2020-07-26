using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsQuestionThread")]
    public class QuestionThread : EntryInfo
    {
        [Key]
        public long QuestionsThreadID { get; set; }


        public long QuestionsID { get; set; }
        public virtual Question Question { get;set;}
        public string UserID { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

    }
}
