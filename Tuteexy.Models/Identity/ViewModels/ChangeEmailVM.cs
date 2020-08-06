using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tuteexy.Models.ViewModels
{
    public class ChangeEmailVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "New email")]
        public string NewEmail { get; set; }

        public long Id { get; set; }
    }
}
