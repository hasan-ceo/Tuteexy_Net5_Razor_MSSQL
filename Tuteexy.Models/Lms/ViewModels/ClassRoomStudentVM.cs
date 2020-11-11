using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models.ViewModels
{
    public class ClassRoomStudentVM
    {
        public long ClassRoomID { get; set; }
        [Required]
        public string StudentEmail { get; set; }
    }
}
