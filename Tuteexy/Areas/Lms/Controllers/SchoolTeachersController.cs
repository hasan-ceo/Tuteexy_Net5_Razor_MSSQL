using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class SchoolTeachersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SchoolTeachersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public SchoolTeachersController(UserManager<IdentityUser> userManager, ILogger<SchoolTeachersController> logger, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.SchoolTeacher.GetAllAsync(t => t.School.OwnerId == _userId, includeProperties: "School,Teacher");

            return View(allObj);
        }

        public IActionResult AddTeacher(long Id)
        {
            SchoolTeacherVM st = new SchoolTeacherVM { 
                SchoolID=Id,
                TeacherEmail=""
            };
            return View(st);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher(SchoolTeacherVM stVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(stVM.TeacherEmail);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["StatusMessage"] = $"Error : Please enter correct teacher's email.";
                    return View(stVM);
                }

                var tmp=await _unitOfWork.SchoolTeacher.GetFirstOrDefaultAsync(s=>s.TeacherID == user.Id && s.SchoolID==stVM.SchoolID);
                if (tmp == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["StatusMessage"] = $"Error : Teacher already added.";
                    return View(stVM);
                }

                var st = new SchoolTeacher
                {
                    SchoolID = stVM.SchoolID,
                    TeacherID = user.Id,
                    ApprovedBy = User.Identity.Name,
                    ApprovedDate = DateTime.Now,
                    IsApproved = true
                };
                await _unitOfWork.SchoolTeacher.AddAsync(st);
                _unitOfWork.Save();

                TempData["StatusMessage"] = $"Successfully add teacher's email";
                return LocalRedirect("/Lms/Schools/Index");
            }
            return View(stVM);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.SchoolTeacher.GetAllAsync(t => t.School.OwnerId == _userId, includeProperties: "School,Teacher");
            return Json(new { data = allObj.Select(o => new { id = o.SchoolTeacherID, schoolname = o.School.SchoolName, fullname = o.Teacher.FullName, approvedby = o.ApprovedBy, isapproved = o.IsApproved }) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.SchoolTeacher.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.SchoolTeacher.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}