using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsSubject")]
    public class Subject : EntryInfo

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
