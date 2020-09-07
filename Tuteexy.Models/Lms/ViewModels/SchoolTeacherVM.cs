using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models.ViewModels
{
    public class SchoolTeacherVM
    {
        public long SchoolID { get; set; }
        [Required]
        public string TeacherEmail { get; set; }
    }
}
