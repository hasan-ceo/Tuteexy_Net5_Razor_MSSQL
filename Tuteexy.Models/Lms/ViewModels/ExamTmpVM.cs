using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ExamTmpVM
    {
        public ExamTmp ExamTmp { get; set; }

        public DateTime ScheduleTime { get; set; }
        public IEnumerable<SelectListItem> ClassRoomList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}
