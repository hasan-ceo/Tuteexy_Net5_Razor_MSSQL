using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class HomeworkReceival : EntryInfo
    {
        [Key]
        public long HomeworkReceivalID { get; set; }
        public long HomeworkID { get; set; }
        public long StudentID { get; set; }

        [Required]
        [MaxLength(64)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Student Homework Title")]
        public string TitleHomeworkStudent { get; set; }
        public DateTime DateSubmitted { get; set; }

        [DataType(DataType.Html)]
        [MaxLength(128)]
        public string Body { get; set; }

        [MaxLength(150)]
        [Display(Name = "Attachment 1")]
        public string Attachment { get; set; }

        [MaxLength(150)]
        [Display(Name = "Attachment 2")]
        public string Attachment1 { get; set; }

        [MaxLength(150)]
        [Display(Name = "Attachment 3")]
        public string Attachment2 { get; set; }

    }
}
