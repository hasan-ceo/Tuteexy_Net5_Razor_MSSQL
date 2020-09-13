using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class ExamsController : Controller
    {
        private readonly ILogger<ExamsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;


        public ExamsController(ILogger<ExamsController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var school = await _unitOfWork.School.GetFirstOrDefaultAsync(s => s.OwnerId == _userId);
            if (school != null)
            {
                var allObj = await _unitOfWork.Exam.GetAllAsync(h => h.ClassRoom.School.OwnerId == _userId, h => h.OrderByDescending(p => p.TimeStart), includeProperties: "ClassRoom,Teacher");
                return View(allObj);
            }
            else
            {
                var allObj = await _unitOfWork.Exam.GetAllAsync(h => h.TeacherID == _userId, h => h.OrderByDescending(p => p.TimeStart), includeProperties: "ClassRoom,Teacher");
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
                return LocalRedirect("/Lms/Exams/Index");
            }
            var listOfIds = y.Select(a => a.SchoolID);

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => listOfIds.Contains(c.SchoolID), includeProperties: "School");
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/Exams/Index");
            }

            IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => listOfIds.Contains(c.SchoolID));
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create subject from manage school";
                return LocalRedirect("/Lms/Exams/Index");
            }

            var hw = new Exam();
            hw.TimeStart = DateTime.Now;
            hw.TimeEnd = DateTime.Now;
            ExamVM examVM = new ExamVM()
            {
                Exam = hw,
                TimeStart = DateTime.Now,
                TimeEnd = DateTime.Now,
                ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.School.ShortName + "-" + i.ClassRoomName,
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
                return View(examVM);
            }
            //this is for edit
            examVM.Exam = await _unitOfWork.Exam.GetAsync(Id.GetValueOrDefault());
            examVM.TimeStart = examVM.Exam.TimeStart;
            examVM.TimeEnd = examVM.Exam.TimeEnd;
            if (examVM.Exam == null)
            {
                return NotFound();
            }
            return View(examVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                if (examVM.Exam.ExamID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == _userId);
                    examVM.Exam.TeacherID = _userId;
                    examVM.Exam.TeacherName = userFromDb.FullName;
                    examVM.Exam.DateAssigned = DateTime.Now;
                    var t = examVM.Exam.TimeStart;
                    examVM.Exam.TimeStart = t.Add(examVM.TimeStart.TimeOfDay);
                    examVM.Exam.TimeEnd = t.Add(examVM.TimeEnd.TimeOfDay);
                    await _unitOfWork.Exam.AddAsync(examVM.Exam);
                }
                else
                {
                    var t = examVM.Exam.TimeStart;
                    examVM.Exam.TimeStart = t.Add(examVM.TimeStart.TimeOfDay);
                    examVM.Exam.TimeEnd = t.Add(examVM.TimeEnd.TimeOfDay);
                    _unitOfWork.Exam.Update(examVM.Exam);
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

                examVM.ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.School.ShortName + "-" + i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                });
                examVM.SubjectList = SubList.Select(i => new SelectListItem
                {
                    Text = i.SubjectName,
                    Value = i.SubjectName
                });
                if (examVM.Exam.ExamID != 0)
                {
                    examVM.Exam = await _unitOfWork.Exam.GetAsync(examVM.Exam.ExamID);
                }
            }
            return View(examVM);
        }


        public async Task<IActionResult> AddQuestion(long Id)
        {
            var eql = await _unitOfWork.ExamQuestion.GetAllAsync(e=>e.ExamID==Id);
            ExamQuestionVM eq = new ExamQuestionVM
            {
                ExamQuestion = new ExamQuestion { ExamID = Id },
                ExamQuestionList=eql
            };
            return View(eq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(ExamQuestionVM examquestionVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\questions");
                    var extenstion = Path.GetExtension(files[0].FileName);


                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    examquestionVM.ExamQuestion.ImageUrl = fileName + extenstion;
                }
                else
                {
                    examquestionVM.ExamQuestion.ImageUrl ="";

                }

                if (examquestionVM.ExamQuestion.ExamQuestionID == 0)
                {
                    if (string.IsNullOrEmpty(examquestionVM.ExamQuestion.Option1)==true && string.IsNullOrWhiteSpace(examquestionVM.ExamQuestion.Option1)==true)
                    {
                        examquestionVM.ExamQuestion.Option1 = "";
                    }
                    if (string.IsNullOrEmpty(examquestionVM.ExamQuestion.Option2) == true && string.IsNullOrWhiteSpace(examquestionVM.ExamQuestion.Option2) == true)
                    {
                        examquestionVM.ExamQuestion.Option2 = "";
                    }
                    if (string.IsNullOrEmpty(examquestionVM.ExamQuestion.Option3) == true && string.IsNullOrWhiteSpace(examquestionVM.ExamQuestion.Option3) == true)
                    {
                        examquestionVM.ExamQuestion.Option3 = "";
                    }
                    if (string.IsNullOrEmpty(examquestionVM.ExamQuestion.Option4) == true && string.IsNullOrWhiteSpace(examquestionVM.ExamQuestion.Option4) == true)
                    {
                        examquestionVM.ExamQuestion.Option4 = "";
                    }
                    if (string.IsNullOrEmpty(examquestionVM.ExamQuestion.CorrectAnswer) == true && string.IsNullOrWhiteSpace(examquestionVM.ExamQuestion.CorrectAnswer) == true)
                    {
                        examquestionVM.ExamQuestion.CorrectAnswer = "";
                    }
                    await _unitOfWork.ExamQuestion.AddAsync(examquestionVM.ExamQuestion);
                }

                _unitOfWork.Save();
                
            }

            return RedirectToAction("AddQuestion", new { Id = examquestionVM.ExamQuestion.ExamID });
        }

        
        #region API CALLS

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Exam.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Exam.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        [HttpDelete]
        public async Task<IActionResult> ExamQuestionDelete(long id)
        {
            var objFromDb = await _unitOfWork.ExamQuestion.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (objFromDb.ImageUrl != null)
            {
                //this is an edit and we need to remove old image
                string webRootPath = _hostEnvironment.WebRootPath;
                var uploads = Path.Combine(webRootPath, @"images\questions");
                var imagePath = Path.Combine(uploads, objFromDb.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _unitOfWork.ExamQuestion.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}