using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class randomComp
    {
        [Key]
        public int randomCompId { get; set; }

        [Display(Name="Category Name")]
        [Required]
        [MaxLength(50)]
        public string Topic { get; set; }
        public string advantage { get; set; }
        public string disadvantage { get; set; }
    }
}
