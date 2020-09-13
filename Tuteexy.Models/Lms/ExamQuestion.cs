using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuteexy.Models
{
    [Table("LmsExamQuestion")]
    public class ExamQuestion
    {
        [Key]
        public long ExamQuestionID { get; set; }

        public long ExamID { get; set; }
        [ForeignKey("ExamID")]
        public virtual Exam Exam { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Title")]
        public string Title { get; set; }

       // [Required(ErrorMessage = "Select Option 1")]
        [MaxLength(150)]
        [Display(Name = "Option 1")]
        public string Option1 { get; set; }

        //[Required(ErrorMessage = "Select Option 2")]
        [MaxLength(150)]
        [Display(Name = "Option 2")]
        public string Option2 { get; set; }

       // [Required(ErrorMessage = "Select Option 3")]
        [MaxLength(150)]
        [Display(Name = "Option 3")]
        public string Option3 { get; set; }

       // [Required(ErrorMessage = "Select Option 4")]
        [MaxLength(150)]
        [Display(Name = "Option 4")]
        public string Option4 { get; set; }

       // [Required(ErrorMessage = "Correct Answer")]
        [MaxLength(150)]
        [Display(Name = "Correct Answer")]
        public string CorrectAnswer { get; set; }


        [MaxLength(150)]
        public string ImageUrl { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Marks")]
        public double Marks { get; set; }

        [MaxLength(150)]
        public string Qtype { get; set; }

    }
}
