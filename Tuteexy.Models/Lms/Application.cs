using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models
{
    public class Application : EntryInfo
    {
        [Key]
        public long ApplicationID { get; set; }
        public long SchoolID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
    }
}
