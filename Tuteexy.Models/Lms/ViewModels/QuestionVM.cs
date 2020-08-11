using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class QuestionVM
    {
        public Question Question { get; set; }

        public IEnumerable<QuestionThread> QuestionThread { get; set; }

        public string UserId { get; set; }

    }
}
