using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ClassRoutineVM
    {
        public ClassRoutine ClassRoutine { get; set; }

        public IEnumerable<SelectListItem> ClassRoomList { get; set; }
        public IEnumerable<SelectListItem> DayList { get; set; }

        public IEnumerable<SelectListItem> P1List { get; set; }
        public IEnumerable<SelectListItem> P2List { get; set; }
        public IEnumerable<SelectListItem> P3List { get; set; }
        public IEnumerable<SelectListItem> P4List { get; set; }
        public IEnumerable<SelectListItem> P5List { get; set; }
        public IEnumerable<SelectListItem> P6List { get; set; }
        public IEnumerable<SelectListItem> P7List { get; set; }
        public IEnumerable<SelectListItem> P8List { get; set; }
        public IEnumerable<SelectListItem> P9List { get; set; }
        public IEnumerable<SelectListItem> P10List { get; set; }
    }
}
