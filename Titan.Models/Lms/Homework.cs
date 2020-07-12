using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Homework
    {
        [Key]
        public long HomeworkID { get; set; }
        public long ClassID { get; set; }
        [Required]
        [MaxLength(64)]
        [Display(Name = "Classwork Title")]
        public string Subject { get; set; }
        [Required]
        [MaxLength(128)]
        [Display(Name = "Homework Title")]
        public string Title { get; set; }

        //Teacher who has assigned a homework
        [Required]
        [MaxLength(128)]
        [Display(Name = "Homework assigned by:")]
        public string Assignedby { get; set; }

        public DateTime DateAssigned { get; set; }
        public DateTime DateDue { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(4096)]
        public string Body { get; set; }

        [MaxLength(150)]
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }

    }
}
