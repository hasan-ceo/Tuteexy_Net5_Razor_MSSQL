using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
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

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult UnderConstruction()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            long classrooomid = 0;
            long schoolid = 0;
            var _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroomStudents = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            if (classroomStudents != null)
            {
                classrooomid = classroomStudents.ClassRoomID;
                var classRoom = await _unitOfWork.ClassRoom.GetFirstOrDefaultAsync(c => c.ClassRoomID == classroomStudents.ClassRoomID);
                schoolid = classRoom.SchoolID;
            }
            
            var question = await _unitOfWork.Question.GetAllAsync(h =>h.IsApproved==true && h.IsOffensive==false &&  h.SubmittedDate.Date >= DateTime.Now.AddDays(-2) && h.SubmittedDate.Date<=DateTime.Now, h => h.OrderByDescending(p => p.SubmittedDate), includeProperties: "User");


            UserHomeVM userhome = new UserHomeVM()
            {
                Question=question
            };


            return View(userhome);
        }

       
    }
}