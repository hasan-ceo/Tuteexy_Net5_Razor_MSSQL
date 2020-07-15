using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsClassRooms")]
    public class ClassRoom : EntryInfo

    {
        [Key]
        public long ClassRoomID { get; set; }
        public long SchoolID { get; set; }
        public virtual School School { get; set; }

        [Display(Name = "Class Room")]
        [Required]
        [MaxLength(100)]
        public string ClassRoomName { get; set; }


    }
}
