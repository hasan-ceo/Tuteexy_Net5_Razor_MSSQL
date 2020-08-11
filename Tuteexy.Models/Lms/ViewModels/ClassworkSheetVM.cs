using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ClassworkSheetVM
    {
        public Classwork Classwork { get; set; }
        public IEnumerable<ClassworkSheet> ClassworkSheet { get; set; }
    }
}
