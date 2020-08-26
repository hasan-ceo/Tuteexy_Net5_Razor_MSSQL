using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class MyClassworksController : Controller
    {
        private readonly ILogger<MyClassworksController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;

        public MyClassworksController(ILogger<MyClassworksController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var allObj = await _unitOfWork.ClassworkSheet.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View();
        }

        public async Task<IActionResult> LiveClass(long Id)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == _userId);

            ChatVM chatVM = new ChatVM
            {
                UserName = userFromDb.FullName,
                GroupName = "Class" + Id.ToString()
            };
           //var allObj = await _unitOfWork.ClassworkSheet.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View("LiveClass", chatVM);
        }

        [HttpGet]
        public async Task<IActionResult> ClassWork()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.Classwork.GetAllAsync(h => h.ClassRoomID == classroomID && h.TimeStart <= DateTime.Now, h => h.OrderByDescending(p => p.TimeStart), includeProperties: "ClassRoom,Teacher");
            return View(allObj.OrderByDescending(a => a.ClassworkID));

        }

        public async Task<IActionResult> ClassWorkReply(long? Id)
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
        public async Task<IActionResult> ClassWorkReply(ClassworkSheet questionthread)
        {
            if (ModelState.IsValid)
            {
                if (questionthread.ClassworkSheetID == 0)
                {
                    questionthread.SubmittedDate = DateTime.Now;
                    questionthread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    questionthread.AttnStatus = "";
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
            return RedirectToAction("ClassworkReply", questionthread.ClassworkSheetID);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassWorkAtten(ClassworkSheet questionthread)
        {
            if (ModelState.IsValid)
            {
                if (questionthread.ClassworkSheetID == 0)
                {
                    questionthread.SubmittedDate = DateTime.Now;
                    questionthread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    questionthread.AttnStatus = "";
                    var tmpQ = await _unitOfWork.ClassworkSheet.GetFirstOrDefaultAsync(c => c.ClassworkID == questionthread.ClassworkID && c.UserID== questionthread.UserID && (c.AttnStatus== SD.AttnStatusPresent || c.AttnStatus==SD.AttnStatusLate || c.AttnStatus==SD.AttnStatusAbsent));
                    if (tmpQ==null)
                    {
                        var ct = await _unitOfWork.Classwork.GetFirstOrDefaultAsync(c => c.ClassworkID == questionthread.ClassworkID);
                        if (DateTime.Now >= ct.TimeStart && DateTime.Now <= ct.TimeStart.AddMinutes(1))
                        {
                            questionthread.AttnStatus = SD.AttnStatusPresent;
                        }
                        else if (DateTime.Now >= ct.TimeStart.AddMinutes(1) && DateTime.Now <= ct.TimeEnd)
                        {
                            questionthread.AttnStatus = SD.AttnStatusLate;
                        }
                        else
                        {
                            questionthread.AttnStatus = SD.AttnStatusAbsent;
                        }
                    }
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
            var t = await _unitOfWork.Classwork.GetFirstOrDefaultAsync(c=>c.ClassworkID== questionthread.ClassworkID);
            return Redirect(t.RefLink1);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ClassworkSheet.GetAllAsync(c => c.UserID == _userId);
            return Json(new { data = allObj.Select(a => new { id = a.ClassworkSheetID, description = a.Description, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ClassworkSheet.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ClassworkSheet.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}