using System;

namespace Tuteexy.Models.ViewModels
{
    public class RptExamTmpVM
    {
        public string ClassRoomName { get; set; }
        public string TeacherName { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public DateTime DateDue { get; set; }
        public int TotalStudent { get; set; }
        public int TotalPending { get; set; }
        public int TotalSubmitted { get; set; }
        public int TotalAccepted { get; set; }
    }
}
