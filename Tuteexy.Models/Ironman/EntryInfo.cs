using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tuteexy.Models
{
    public class EntryInfo
    {
        //User who entry first
        [MaxLength(50)]
        public string CreatedBy { get; set; }

        //Business Date of first entry

        [Required(ErrorMessage = "Please enter Created Date")]
        [Column(TypeName = "datetime")]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        //User who update it last time
        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        //Business Date of Last Update
        [Required(ErrorMessage = "Please enter Updated Date")]
        [Column(TypeName = "datetime")]
        [Display(Name = "Updated Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }
    }
}
