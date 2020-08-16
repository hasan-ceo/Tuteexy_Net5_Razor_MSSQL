using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
    [Authorize(Roles = SD.Role_User)]
    public class LiveMeetingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
