using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ExamVM
    {
        public Exam Exam { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public IEnumerable<SelectListItem> ClassRoomList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}
