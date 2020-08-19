using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class RptHomeworkVM
    {
        public string ClassRoomName { get; set; }
        public string TeacherName { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public DateTime DateDue { get; set; }
        public long TotalStudent { get; set; }
        public long TotalPending { get; set; }
        public long TotalSubmitted { get; set; }
        public long TotalAccepted { get; set; }
    }
}
