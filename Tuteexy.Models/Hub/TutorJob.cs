using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    [Table("LmsTutorJob")]
    public class TutorJob : EntryInfo

    {
        [Key]
        public long TutorJobID { get; set; }

        [Display(Name = "Title")]
        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; }

        [Display(Name = "Course")]
        [Required]
        [MaxLength(100)]
        public string Course { get; set; }

        [Display(Name = "Subject")]
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Display(Name = "Salary")]
        [Required]
        [MaxLength(100)]
        public string Salary { get; set; }

        [Display(Name = "Number of Students")]
        [Required]
        [MaxLength(100)]
        public string NumberofStudents { get; set; }

        [Display(Name = "Gender preference")]
        [Required]
        [MaxLength(100)]
        public string Genderpreference { get; set; }

        [Display(Name = "Requirements")]
        [Required]
        [MaxLength(250)]
        public string Requirements { get; set; }

        [Display(Name = "Street Address")]
        [Required]
        [MaxLength(100)]
        public string StreetAddress { get; set; }

        [MaxLength(100)]
        [Required]
        public string City { get; set; }

        [MaxLength(100)]
        [Required]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        [Required]
        [MaxLength(100)]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(50)]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
