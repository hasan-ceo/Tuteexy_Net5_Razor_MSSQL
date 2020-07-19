using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class SchoolNoticeVM
    {
        public SchoolNotice SchoolNotice { get; set; }
        public string ScheduleTime { get; set; }
    }
}
