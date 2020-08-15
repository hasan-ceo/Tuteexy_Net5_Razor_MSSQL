using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class CourseVM
    {
        public Course Course { get; set; }

        public IEnumerable<CourseThread> CourseThread { get; set; }

        public string UserId { get; set; }

    }
}
