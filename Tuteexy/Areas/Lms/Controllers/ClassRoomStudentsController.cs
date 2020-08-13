using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class ClassRoomStudentsController : Controller
    {
        private readonly ILogger<ClassRoomStudentsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassRoomStudentsController(ILogger<ClassRoomStudentsController> logger, IUnitOfWork unitOfWork)
        {
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
                var workdate= DateTime.Now;


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

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.ClassRoomStudent.GetAllAsync(t => t.ClassRoom.School.OwnerId == _userId, includeProperties: "ClassRoom,Student");
            return Json(new { data = allObj.Select(o=> new { id=o.ClassRoomStudentID, classroomname=o.ClassRoom.ClassRoomName, fullname=o.Student.FullName, approvedby=o.ApprovedBy, isapproved=o.IsApproved}) });
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