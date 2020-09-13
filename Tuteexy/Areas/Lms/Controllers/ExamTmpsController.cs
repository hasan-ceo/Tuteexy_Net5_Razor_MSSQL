using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
    public class ExamTmpsController : Controller
    {
        private readonly ILogger<ExamTmpsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ExamTmpsController(ILogger<ExamTmpsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var school = await _unitOfWork.School.GetFirstOrDefaultAsync(s => s.OwnerId == _userId);
            if (school != null)
            {
                var allObj = await _unitOfWork.ExamTmp.GetAllAsync(h => h.ClassRoom.School.OwnerId == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return View(allObj);
            }
            else
            {
                var allObj = await _unitOfWork.ExamTmp.GetAllAsync(h => h.TeacherID == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return View(allObj);
            }
        }





        public async Task<IActionResult> Upsert(long? Id)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var y = await _unitOfWork.SchoolTeacher.GetAllAsync(t => t.TeacherID == _userId);

            if (y == null)
            {
                TempData["StatusMessage"] = $"Error : Please register as teacher";
                return LocalRedirect("/Lms/ExamTmps/Index");
            }
            var listOfIds = y.Select(a=>a.SchoolID);

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => listOfIds.Contains(c.SchoolID), includeProperties:"School");
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/ExamTmps/Index");
            }

            IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => listOfIds.Contains(c.SchoolID));
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create subject from manage school";
                return LocalRedirect("/Lms/ExamTmps/Index");
            }

            var hw = new ExamTmp();
            hw.ScheduleDateTime = DateTime.Now;
            hw.DateDue = DateTime.Now;
            ExamTmpVM examtmpVM = new ExamTmpVM()
            {
                ExamTmp = hw,
                ScheduleTime = DateTime.Now,
                ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text =i.School.ShortName + "-" + i.ClassRoomName,
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
                return View(examtmpVM);
            }
            //this is for edit
            examtmpVM.ExamTmp = await _unitOfWork.ExamTmp.GetAsync(Id.GetValueOrDefault());
            examtmpVM.ScheduleTime = examtmpVM.ExamTmp.ScheduleDateTime;
            if (examtmpVM.ExamTmp == null)
            {
                return NotFound();
            }
            return View(examtmpVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ExamTmpVM examtmpVM)
        {
            if (ModelState.IsValid)
            {
                if (examtmpVM.ExamTmp.ExamTmpID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == _userId);
                    examtmpVM.ExamTmp.TeacherID = _userId;
                    examtmpVM.ExamTmp.TeacherName = userFromDb.FullName;
                    examtmpVM.ExamTmp.DateAssigned = DateTime.Now;
                    examtmpVM.ExamTmp.ScheduleDateTime = examtmpVM.ExamTmp.ScheduleDateTime.Add(examtmpVM.ScheduleTime.TimeOfDay);
                    await _unitOfWork.ExamTmp.AddAsync(examtmpVM.ExamTmp);
                }
                else
                {
                    examtmpVM.ExamTmp.ScheduleDateTime = examtmpVM.ExamTmp.ScheduleDateTime.Add(examtmpVM.ScheduleTime.TimeOfDay);
                    _unitOfWork.ExamTmp.Update(examtmpVM.ExamTmp);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var y = await _unitOfWork.SchoolTeacher.GetAllAsync(t => t.TeacherID == _userId);
                var listOfIds = y.Select(a => a.SchoolID);
                IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => listOfIds.Contains(c.SchoolID), includeProperties: "School");
                IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => listOfIds.Contains(c.SchoolID));

                examtmpVM.ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.School.ShortName + "-" + i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                });
                examtmpVM.SubjectList = SubList.Select(i => new SelectListItem
                {
                    Text = i.SubjectName,
                    Value = i.SubjectName
                });
                if (examtmpVM.ExamTmp.ExamTmpID != 0)
                {
                    examtmpVM.ExamTmp = await _unitOfWork.ExamTmp.GetAsync(examtmpVM.ExamTmp.ExamTmpID);
                }
            }
            return View(examtmpVM);
        }


        public async Task<IActionResult> ExmPreview(long id)
        {
            var t = await _unitOfWork.ExamTmp.GetFirstOrDefaultAsync(h => h.ExamTmpID == id, includeProperties: "ClassRoom,Teacher");
            return View(t);
        }

        public async Task<IActionResult> ReplyList()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var examtmp = await _unitOfWork.ExamTmp.GetAllAsync(h => h.TeacherID == _userId, includeProperties: "ClassRoom");
            var examtmpsheet = await _unitOfWork.ExamTmpSheet.GetAllAsync(h => h.ExamTmp.TeacherID == _userId, includeProperties: "ExamTmp,Student");



            var allObj = from h in examtmp
                         join s in examtmpsheet on h.ExamTmpID equals s.ExamTmpID
                         select new ExamTmpSheetVM { ExamTmpSheetID = s.ExamTmpSheetID, ClassRoomName = h.ClassRoom.ClassRoomName, Subject = h.Subject, Title = h.Title, ScheduleDateTime = h.ScheduleDateTime, DateDue = h.DateDue, StudentName = s.Student.FullName, ExmMarks = h.ExmMarks, ExmStatus = s.ExmStatus, AttachLink1 = s.AttachLink1 };

            return View(allObj.OrderBy(a => a.ClassRoomName).OrderByDescending(a => a.ExmStatus));
        }

        public async Task<IActionResult> MarkUpdate(long Id)
        {
            var ExamTmpSheet = await _unitOfWork.ExamTmpSheet.GetFirstOrDefaultAsync(h => h.ExamTmpSheetID == Id, includeProperties: "ExamTmp,Student");
            return View(ExamTmpSheet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkUpdate(ExamTmpSheet hws)
        {
            var hwr = await _unitOfWork.ExamTmpSheet.GetFirstOrDefaultAsync(i => i.ExamTmpSheetID == hws.ExamTmpSheetID, includeProperties: "ExamTmp,Student");
            if (hwr == null)
            {
                TempData["StatusMessage"] = $"Error : Please check examtmp";
                return LocalRedirect("/Lms/ExamTmps/ReplyList");
            }

            //if (hws.HwMarks ==0)
            //{
            //    TempData["StatusMessage"] = $"Error : Marks can not be zero";
            //    return RedirectToAction("MarkUpdate", new { Id=hws.HwMarks });

            //}

            if (hws.ExmMarks > hwr.ExamTmp.ExmMarks)
            {
                TempData["StatusMessage"] = $"Error : Given marks can not be more than ExamTmp marks.";
                return RedirectToAction("MarkUpdate", new { Id = hws.ExamTmpSheetID });

            }

            hwr.ExmMarks = hws.ExmMarks;
            hwr.DateSubmitted = DateTime.Now;
            hwr.ExmStatus = SD.StatusAccepted;

            if (hwr.ExamTmpSheetID != 0)
            {
                _unitOfWork.ExamTmpSheet.Update(hwr);
                _unitOfWork.Save();
            }

            return RedirectToAction("ReplyList");
        }

        public IActionResult Hwd(long Id)
        {
            return ViewComponent("QuestionA", new { id = Id });
        }

        public IActionResult RptExamTmps()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var parameter = new DynamicParameters();
            parameter.Add("@AdminId", _userId);
            var rptexamtmpVM = _unitOfWork.SP_Call.List<RptExamTmpVM>(SD.Proc_rptExamTmp, parameter);
            return View(rptexamtmpVM.OrderBy(r => r.ClassRoomName).ThenByDescending(r => r.DateDue));
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var school = await _unitOfWork.School.GetFirstOrDefaultAsync(s => s.OwnerId == _userId);
            if (school != null)
            {
                var allObj = await _unitOfWork.ExamTmp.GetAllAsync(h => h.ClassRoom.School.OwnerId == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id = a.ExamTmpID, teachername = a.Teacher.FullName, classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, schdate = a.ScheduleDateTime.ToString("dd/MMM/yyyy hh:mm tt"), datedue = a.DateDue.Date.ToString("dd/MMM/yyyy") }) });
            }
            else
            {
                var allObj = await _unitOfWork.ExamTmp.GetAllAsync(h => h.TeacherID == _userId, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
                return Json(new { data = allObj.Select(a => new { id = a.ExamTmpID, teachername = a.Teacher.FullName, classroomname = a.ClassRoom.ClassRoomName, subject = a.Subject, title = a.Title, schdate = a.ScheduleDateTime.ToString("dd/MMM/yyyy hh:mm tt"), datedue = a.DateDue.Date.ToString("dd/MMM/yyyy") }) });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ExamTmp.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ExamTmp.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}