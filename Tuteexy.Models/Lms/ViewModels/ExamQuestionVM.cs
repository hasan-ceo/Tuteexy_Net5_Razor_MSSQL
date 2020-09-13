using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ExamQuestionVM
    {
        public ExamQuestion ExamQuestion { get; set; }
        public IEnumerable<ExamQuestion> ExamQuestionList { get; set; }
    }
}
