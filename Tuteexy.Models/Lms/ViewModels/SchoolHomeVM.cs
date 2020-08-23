using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class SchoolHomeVM
    {
        public ChatVM ChatVM { get; set; }
        public IEnumerable<Homework> Homework { get; set; }
        public IEnumerable<Classwork> Classwork { get; set; }
        public IEnumerable<SchoolNotice> SchoolNotice { get; set; }
        public IEnumerable<ClassRoomNotice> ClassRoomNotice { get; set; }
    }
}
