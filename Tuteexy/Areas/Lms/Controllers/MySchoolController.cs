using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class MySchoolController : Controller
    {
        private readonly ILogger<MySchoolController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;

        public MySchoolController(ILogger<MySchoolController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Classroutines()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.ClassRoutine.GetAllAsync(t => t.ClassRoomID == classroomID, includeProperties: "ClassRoom");
            return View(allObj);
        }

        public async Task<IActionResult> Holidays()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId, includeProperties: "ClassRoom");
            long schoolID = 0;
            if (classroom != null)
            {
                schoolID = classroom.ClassRoom.SchoolID;
            }
            var allObj = await _unitOfWork.Holiday.GetAllAsync(c => c.School.SchoolID == schoolID, includeProperties: "School");

            return View(allObj);
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
                    p1 = o.Period1 + " " + o.PeriodTime1.ToString("hh:mm tt"),
                    p2 = o.Period2 + " " + o.PeriodTime2.ToString("hh:mm tt"),
                    p3 = o.Period3 + " " + o.PeriodTime3.ToString("hh:mm tt"),
                    p4 = o.Period4 + " " + o.PeriodTime4.ToString("hh:mm tt"),
                    p5 = o.Period5 + " " + o.PeriodTime5.ToString("hh:mm tt"),
                    p6 = o.Period6 + " " + o.PeriodTime6.ToString("hh:mm tt"),
                    p7 = o.Period7 + " " + o.PeriodTime7.ToString("hh:mm tt"),
                    p8 = o.Period8 + " " + o.PeriodTime8.ToString("hh:mm tt"),
                    p9 = o.Period9 + " " + o.PeriodTime9.ToString("hh:mm tt"),
                    p10 = o.Period10 + " " + o.PeriodTime10.ToString("hh:mm tt")
                })
            });

        }





        [HttpGet]
        public async Task<IActionResult> ClassNotices()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.ClassRoomNotice.GetAllAsync(h => h.ClassRoomID == classroomID, h => h.OrderByDescending(p => p.ScheduleDateTime), includeProperties: "ClassRoom");
            return View(allObj);

        }

        [HttpGet]
        public async Task<IActionResult> SchoolNotices()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId, includeProperties: "ClassRoom");
            long schoolID = 0;
            if (classroom != null)
            {
                schoolID = classroom.ClassRoom.SchoolID;
            }
            var allObj = await _unitOfWork.SchoolNotice.GetAllAsync(h => h.SchoolID == schoolID, h => h.OrderByDescending(p => p.ScheduleDateTime), includeProperties: "School");
            return View(allObj);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllHoliday()
        {
            //_userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(includeProperties: "ClassRoom");
            long schoolID = 0;
            if (classroom != null)
            {
                schoolID = classroom.ClassRoom.SchoolID;
            }
            var allObj = await _unitOfWork.Holiday.GetAllAsync(c => c.School.SchoolID == schoolID, includeProperties: "School");
            return Json(new { data = allObj.Select(a => new { id = a.HolidayID, schoolname = a.School.SchoolName, datestart = a.DateStart.ToString("dd/MMM/yyyy"), dateend = a.DateEnd.ToString("dd/MMM/yyyy"), holidayname = a.HolidayName, duration = a.Duration }) });

        }

    }
}