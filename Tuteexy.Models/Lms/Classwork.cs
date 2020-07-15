using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tuteexy.Models
{
    public class Classwork : EntryInfo
    {
        [Key]
        public long ClassworkID { get; set; }
        [Required]
        [MaxLength(64)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(128)]
        [Display(Name = "Classwork Title")]
        public string Title { get; set; }
        
        //Teacher who has assigned a homework
        [Required]
        [MaxLength(128)]
        [Display(Name = "Classwork assigned by:")]
        public string Assignedby { get; set; }

        public DateTime DatePublished { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(4096)]
        public string Body { get; set; }

        [MaxLength(150)]
        [Display(Name = "Attachment 1")]
        public string Attachment { get; set; }
    }
}
