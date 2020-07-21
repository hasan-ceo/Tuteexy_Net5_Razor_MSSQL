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
        private string _userId;
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

        public IActionResult Homeworks()
        {
            return View();
        }

        public IActionResult Classroutines()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllClassRoutine()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.ClassRoutine.GetAllAsync(t => t.ClassRoomID == classroomID, includeProperties: "ClassRoom");

                return Json(new
                {
                    data = allObj.Select(o => new
                    {
                        id = o.ClassRoutineID,
                        classname = o.ClassRoom.ClassRoomName,
                        day = o.DayName,
                        p1 = o.Period1,
                        p2 = o.Period2,
                        p3 = o.Period3,
                        p4 = o.Period4,
                        p5 = o.Period5,
                        p6 = o.Period6,
                        p7 = o.Period7,
                        p8 = o.Period8,
                        p9 = o.Period9,
                        p10 = o.Period10
                    })
                });
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHomeWorks()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c=>c.StudentID==_userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.Homework.GetAllAsync(h => h.ClassRoomID == classroomID, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
            return Json(new { data = allObj.Select(a => new { a.HomeworkID, a.ClassRoom.ClassRoomName, a.Subject, a.Title, schdate = a.ScheduleDateTime.Date.ToString("dd/MMM/yyyy hh:mm tt") , datedue = a.DateDue.Date.ToString("dd/MMM/yyyy") }) });
        }
    }
}