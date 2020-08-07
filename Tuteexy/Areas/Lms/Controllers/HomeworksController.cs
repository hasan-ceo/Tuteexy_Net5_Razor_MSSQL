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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class HomeworksController : Controller
    {
        private readonly ILogger<HomeworksController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public HomeworksController(ILogger<HomeworksController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var y = await _unitOfWork.SchoolTeacher.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
            if (y == null)
            {
                TempData["StatusMessage"] = $"Error : Please register as teacher";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create subject from manage school";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            var hw = new Homework();
                hw.ScheduleDateTime = DateTime.Now;
                hw.DateDue = DateTime.Now;
                HomeworkVM homeworkVM = new HomeworkVM()
                {
                    Homework = hw,
                    ScheduleTime = DateTime.Now,
                    ClassRoomList = clsList.Select(i => new SelectListItem
                    {
                        Text = i.ClassRoomName,
                        Value = i.ClassRoomID.ToString()
                    }),
                    SubjectList = SubList.Select(i => new SelectListItem
                    {
                        Text = i.SubjectName,
                        Value = i.SubjectName
                    })
                };
                if (Id == null)
                {
                    //this is for create
                    return View(homeworkVM);
                }
                //this is for edit
                homeworkVM.Homework = await _unitOfWork.Homework.GetAsync(Id.GetValueOrDefault());
                homeworkVM.ScheduleTime = homeworkVM.Homework.ScheduleDateTime;
                if (homeworkVM.Homework == null)
                {
                    return NotFound();
                }
                return View(homeworkVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(HomeworkVM homeworkVM)
        {
            if (ModelState.IsValid)
            {
                if (homeworkVM.Homework.HomeworkID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == _userId);
                    homeworkVM.Homework.TeacherID = _userId;
                    homeworkVM.Homework.TeacherName = userFromDb.FullName;
                    homeworkVM.Homework.DateAssigned = DateTime.Now;
                    homeworkVM.Homework.ScheduleDateTime = homeworkVM.Homework.ScheduleDateTime.Add(homeworkVM.ScheduleTime.TimeOfDay);
                    await _unitOfWork.Homework.AddAsync(homeworkVM.Homework);
                }
                else
                {
                    homeworkVM.Homework.ScheduleDateTime = homeworkVM.Homework.ScheduleDateTime.Add(homeworkVM.ScheduleTime.TimeOfDay);
                    _unitOfWork.Homework.Update(homeworkVM.Homework);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var y = await _unitOfWork.SchoolTeacher.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
                IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
                IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => c.SchoolID == y.SchoolID);

                homeworkVM.ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                });
                homeworkVM.SubjectList = SubList.Select(i => new SelectListItem
                {
                    Text = i.SubjectName,
                    Value = i.SubjectName
                });
                if (homeworkVM.Homework.HomeworkID != 0)
                {
                    homeworkVM.Homework = await _unitOfWork.Homework.GetAsync(homeworkVM.Homework.HomeworkID);
                }
            }
            return View(homeworkVM);
        }


        public async Task<IActionResult> HWPreview(long id)
        {
            var t = await _unitOfWork.Homework.GetFirstOrDefaultAsync(h=>h.HomeworkID==id,  includeProperties: "ClassRoom,Teacher");
            return View(t);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var school = await _unitOfWork.School.GetFirstOrDefaultAsync(s => s.OwnerId == _userId);
            if (school !=null)
            {
                var allObj = await _unitOfWork.Homework.GetAllAsync(h => h.ClassRoom.School.OwnerId == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id=a.HomeworkID, teachername=a.Teacher.FullName , classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, schdate = a.ScheduleDateTime.ToString("dd/MMM/yyyy hh:mm tt"), datedue = a.DateDue.Date.ToString("dd/MMM/yyyy") }) });
            }
            else
            {
                var allObj = await _unitOfWork.Homework.GetAllAsync(h => h.TeacherID == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id = a.HomeworkID, teachername = a.Teacher.FullName, classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, schdate = a.ScheduleDateTime.ToString("dd/MMM/yyyy hh:mm tt"), datedue = a.DateDue.Date.ToString("dd/MMM/yyyy") }) });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Homework.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Homework.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}