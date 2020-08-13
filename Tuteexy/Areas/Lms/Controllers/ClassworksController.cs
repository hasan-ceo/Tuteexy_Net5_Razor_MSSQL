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
    public class ClassworksController : Controller
    {
        private readonly ILogger<ClassworksController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassworksController(ILogger<ClassworksController> logger, IUnitOfWork unitOfWork)
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
                return LocalRedirect("/Lms/Classworks/Index");
            }

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/Classworks/Index");
            }

            IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create subject from manage school";
                return LocalRedirect("/Lms/Classworks/Index");
            }

            var cw = new Classwork();
            cw.TimeStart = DateTime.Now;
            cw.TimeEnd = DateTime.Now;
            ClassworkVM classworkVM = new ClassworkVM()
            {
                Classwork = cw,
                TimeStart = DateTime.Now,
                TimeEnd = DateTime.Now,
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
                return View(classworkVM);
            }
            //this is for edit
            classworkVM.Classwork = await _unitOfWork.Classwork.GetAsync(Id.GetValueOrDefault());
            classworkVM.TimeStart = classworkVM.Classwork.TimeStart;
            classworkVM.TimeEnd = classworkVM.Classwork.TimeEnd;
            if (classworkVM.Classwork == null)
            {
                return NotFound();
            }
            return View(classworkVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ClassworkVM classworkVM)
        {
            if (ModelState.IsValid)
            {
                if (classworkVM.Classwork.ClassworkID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    classworkVM.Classwork.TeacherID = _userId;
                    classworkVM.Classwork.DateAssigned = DateTime.Now;
                    var t = classworkVM.Classwork.TimeStart;
                    classworkVM.Classwork.TimeStart = t.Add(classworkVM.TimeStart.TimeOfDay);
                    classworkVM.Classwork.TimeEnd = t.Add(classworkVM.TimeEnd.TimeOfDay); 
                    await _unitOfWork.Classwork.AddAsync(classworkVM.Classwork);
                }
                else
                {
                    var t = classworkVM.Classwork.TimeStart;
                    classworkVM.Classwork.TimeStart = t.Add(classworkVM.TimeStart.TimeOfDay);
                    classworkVM.Classwork.TimeEnd = t.Add(classworkVM.TimeEnd.TimeOfDay);
                    _unitOfWork.Classwork.Update(classworkVM.Classwork);
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

                classworkVM.ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                });
                classworkVM.SubjectList = SubList.Select(i => new SelectListItem
                {
                    Text = i.SubjectName,
                    Value = i.SubjectName
                });
                if (classworkVM.Classwork.ClassworkID != 0)
                {
                    classworkVM.Classwork = await _unitOfWork.Classwork.GetAsync(classworkVM.Classwork.ClassworkID);
                }
            }
            return View(classworkVM);
        }


        public async Task<IActionResult> Answer(long Id)
        {
            var classwork = await _unitOfWork.Classwork.GetFirstOrDefaultAsync(q => q.ClassworkID == Id, includeProperties: "Teacher");
            var classworksheet = await _unitOfWork.ClassworkSheet.GetAllAsync(q => q.ClassworkID == Id, includeProperties: "User");
            ClassworkSheetVM questionVM = new ClassworkSheetVM
            {
                Classwork = classwork,
                ClassworkSheet = classworksheet
            };
            //var allObj = await _unitOfWork.ClassworkSheet.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(questionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(ClassworkSheet questionthread)
        {
            if (ModelState.IsValid)
            {
                if (questionthread.ClassworkSheetID == 0)
                {
                    questionthread.SubmittedDate = DateTime.Now;
                    questionthread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.ClassworkSheet.AddAsync(questionthread);
                }
                else
                {
                    var tmpQ = await _unitOfWork.ClassworkSheet.GetAsync(questionthread.ClassworkSheetID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = questionthread.Description;
                    _unitOfWork.ClassworkSheet.Update(questionthread);
                }

                _unitOfWork.Save();
                //return RedirectToAction("Answer", questionthread.ClassworkSheetID);
            }
            return RedirectToAction("Answer", questionthread.ClassworkSheetID);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var school = await _unitOfWork.School.GetFirstOrDefaultAsync(s => s.OwnerId == _userId);
            if (school != null)
            {
                var allObj = await _unitOfWork.Classwork.GetAllAsync(h => h.ClassRoom.School.OwnerId == _userId, h => h.OrderByDescending(p => p.TimeStart), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id = a.ClassworkID, teachername = a.Teacher.FullName, classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, timestart = a.TimeStart.ToString("dd/MMM/yyyy hh:mm tt"), timeend = a.TimeEnd.ToString("dd/MMM/yyyy hh:mm tt") }) });
            }
            else
            {
                var allObj = await _unitOfWork.Classwork.GetAllAsync(h => h.TeacherID == _userId, h => h.OrderByDescending(p => p.TimeStart), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id = a.ClassworkID, teachername = a.Teacher.FullName, classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, timestart = a.TimeStart.ToString("dd/MMM/yyyy hh:mm tt"), timeend = a.TimeEnd.ToString("dd/MMM/yyyy hh:mm tt") }) });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Classwork.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Classwork.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}