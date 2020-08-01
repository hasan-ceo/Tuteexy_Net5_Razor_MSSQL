using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class SchoolNoticeVM
    {
        public SchoolNotice SchoolNotice { get; set; }
        public DateTime ScheduleTime { get; set; }
    }
}
