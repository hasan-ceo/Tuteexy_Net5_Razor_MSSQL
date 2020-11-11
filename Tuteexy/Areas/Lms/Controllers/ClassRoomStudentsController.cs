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
    public class ClassRoomStudentsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ClassRoomStudentsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassRoomStudentsController(UserManager<IdentityUser> userManager, ILogger<ClassRoomStudentsController> logger, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(long id)
        {
            //_userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.ClassRoomStudent.GetAllAsync(t => t.ClassRoomID == id, includeProperties: "ClassRoom,Student");
            return View(allObj.ToList());
        }

        public async Task<IActionResult> Upsert(long? Id)
        {

            ClassRoomStudent classroomstudent = new ClassRoomStudent();
            if (Id == null)
            {
                //this is for create
                return View(classroomstudent);
            }
            //this is for edit
            classroomstudent = await _unitOfWork.ClassRoomStudent.GetAsync(Id.GetValueOrDefault());
            if (classroomstudent == null)
            {
                return NotFound();
            }
            return View(classroomstudent);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ClassRoomStudent classroomstudent)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;


                if (classroomstudent.ClassRoomStudentID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


                    _unitOfWork.ClassRoomStudent.AddAsync(classroomstudent);

                }
                else
                {



                    _unitOfWork.ClassRoomStudent.Update(classroomstudent);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(classroomstudent);
        }


        public IActionResult AddStudent(long Id)
        {
            ClassRoomStudentVM st = new ClassRoomStudentVM
            { 
                ClassRoomID = Id,
                StudentEmail = ""
            };
            return View(st);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(ClassRoomStudentVM stVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(stVM.StudentEmail);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["StatusMessage"] = $"Error : Please enter correct student's email.";
                    return View(stVM);
                }

                var tmp = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(s => s.StudentID == user.Id && s.ClassRoomID == stVM.ClassRoomID);
                if (tmp != null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    TempData["StatusMessage"] = $"Error : student already added.";
                    return View(stVM);
                }

                var st = new ClassRoomStudent
                {
                    ClassRoomID = stVM.ClassRoomID,
                    StudentID = user.Id,
                    ApprovedBy = User.Identity.Name,
                    ApprovedDate = DateTime.Now,
                    IsApproved = true
                };
                await _unitOfWork.ClassRoomStudent.AddAsync(st);
                _unitOfWork.Save();

                TempData["StatusMessage"] = $"Successfully add student's email";
                return LocalRedirect("/Lms/ClassRoomStudents/Index/"+ st.ClassRoomID);
            }
            return View(stVM);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.ClassRoomStudent.GetAllAsync(t => t.ClassRoom.School.OwnerId == _userId, includeProperties: "ClassRoom,Student");
            return Json(new { data = allObj.Select(o => new { id = o.ClassRoomStudentID, classroomname = o.ClassRoom.ClassRoomName, fullname = o.Student.FullName, approvedby = o.ApprovedBy, isapproved = o.IsApproved }) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ClassRoomStudent.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ClassRoomStudent.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}