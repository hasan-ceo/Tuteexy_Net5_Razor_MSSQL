using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class eclass
    {
        [Key]
        public int eclassID { get; set; }

        [Display(Name="Class Name")]
        [Required]
        [MaxLength(50)]
        public string classname { get; set; }

        public int year { get; set; }

    }
}
