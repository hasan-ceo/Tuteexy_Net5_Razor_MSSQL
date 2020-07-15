using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsSchools")]
    public class School : EntryInfo
    {
        [Key]
        public long SchoolID { get; set; }
        [Display(Name = "School Name")]
        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }
        [Display(Name = "Street Address")]
        [MaxLength(100)]
        public string StreetAddress { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [Display(Name = "Postal Code")]
        [MaxLength(100)]
        public string PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        public bool IsAuthorizedSchool { get; set; }
        
        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

    }
}
