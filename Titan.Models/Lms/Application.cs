using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
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
