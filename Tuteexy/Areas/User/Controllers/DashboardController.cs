using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
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
            var _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroomStudents = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            if (classroomStudents!=null){
                classrooomid = classroomStudents.ClassRoomID;
            }
            var allObj = await _unitOfWork.Homework.GetAllAsync(h => h.ClassRoomID == classrooomid, h=>h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");

            return View(allObj);
        }
    }
}