using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using Titan.Models.ViewModels;
using Titan.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Titan.Areas.Lms.Controllers
{
    [Area("Lms")]
    //[Authorize(Roles = SD.Role_User)]
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
            var y = await _unitOfWork.SchoolTeachers.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRooms.GetAllAsync(c => c.SchoolID == y.SchoolID);
            IEnumerable<Subject> SubList = await _unitOfWork.Subjects.GetAllAsync(c => c.SchoolID == y.SchoolID);
            HomeworkVM homeworkVM = new HomeworkVM()
            {
                Homework = new Homework(),
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
            homeworkVM.Homework = await _unitOfWork.Homeworks.GetAsync(Id.GetValueOrDefault());
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
                    await _unitOfWork.Homeworks.AddAsync(homeworkVM.Homework);

                }
                else
                {
                    _unitOfWork.Homeworks.Update(homeworkVM.Homework);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var y = await _unitOfWork.SchoolTeachers.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
                IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRooms.GetAllAsync(c => c.SchoolID == y.SchoolID);
                IEnumerable<Subject> SubList = await _unitOfWork.Subjects.GetAllAsync(c => c.SchoolID == y.SchoolID);

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
                    homeworkVM.Homework = await _unitOfWork.Homeworks.GetAsync(homeworkVM.Homework.HomeworkID);
                }
            }
            return View(homeworkVM);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Homeworks.GetAllAsync(h=>h.TeacherID==_userId,includeProperties:"ClassRoom,Teacher");
            return Json(new { data = allObj.Select(a=> new { a.ClassRoom.ClassRoomName,a.Subject,a.Title,a.DateDue}) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Homeworks.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Homeworks.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}