using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;

namespace Tuteexy.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(ILogger<DashboardController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            long classrooomid=0;
            long schoolid = 0;
            var _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroomStudents = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            if (classroomStudents!=null){
                classrooomid = classroomStudents.ClassRoomID;
                var classRoom = await _unitOfWork.ClassRoom.GetFirstOrDefaultAsync(c => c.ClassRoomID == classroomStudents.ClassRoomID);
                schoolid = classRoom.SchoolID;
            }
            var homework = await _unitOfWork.Homework.GetAllAsync(h => h.ClassRoomID == classrooomid && h.ScheduleDateTime<=DateTime.Now && h.ScheduleDateTime.Date == DateTime.Now.Date, h=>h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
            var schoolnotice = await _unitOfWork.SchoolNotice.GetAllAsync(h => h.SchoolID == schoolid && h.ScheduleDateTime<= DateTime.Now && h.ScheduleDateTime.Date == DateTime.Now.Date, h => h.OrderByDescending(p => p.ScheduleDateTime), includeProperties: "School");

            UserHomeVM userhome = new UserHomeVM()
            {
                Homework = homework,
                SchoolNotice= schoolnotice
            };

            
            return View(userhome);
        }
    }
}