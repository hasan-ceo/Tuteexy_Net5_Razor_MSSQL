using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Titan.Models
{
    public class School
    {
        [Key]
        public long SchoolID { get; set; }
        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }
        [MaxLength(100)]
        public string StreetAddress { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string State { get; set; }
        [MaxLength(100)]
        public string PostalCode { get; set; }
        [MaxLength(100)]
        public string PhoneNumber { get; set; }

        public bool IsAuthorizedSchool { get; set; }
        [ForeignKey("Id")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
