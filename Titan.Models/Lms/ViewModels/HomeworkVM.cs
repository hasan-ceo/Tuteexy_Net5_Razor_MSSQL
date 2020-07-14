using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Titan.Models.ViewModels
{
    public class HomeworkVM
    {
        public Homework Homework { get; set; }
        public IEnumerable<SelectListItem> ClassRoomList { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}
