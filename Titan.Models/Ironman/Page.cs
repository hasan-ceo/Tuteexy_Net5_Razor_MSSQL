using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Page
    {
        [Key]
        public long PageID { get; set; }

        [Display(Name = "Page Name")]
        [Required]
        [MaxLength(50)]
        public string PageName { get; set; }

        [Display(Name = "Description")]
        [Required]
        [DataType(DataType.Html)]
        public string Description { get; set; }
    }
}
