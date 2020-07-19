using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class UserHomeVM
    {
        public IEnumerable<Homework> Homework { get; set; }
        public IEnumerable<SchoolNotice> SchoolNotice { get; set; }
    }
}
