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

namespace Tuteexy.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class ClassworkSheetsController : Controller
    {
        private readonly ILogger<ClassworkSheetsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;

        public ClassworkSheetsController(ILogger<ClassworkSheetsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var allObj = await _unitOfWork.ClassworkSheet.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View();
        }


        public async Task<IActionResult> Answer(long? Id)
        {
            var classwork = await _unitOfWork.Classwork.GetFirstOrDefaultAsync(q=>q.ClassworkID==Id,includeProperties: "Teacher");
            var classworksheet = await _unitOfWork.ClassworkSheet.GetAllAsync(q => q.ClassworkID == Id, includeProperties: "User");
            ClassworkSheetVM questionVM = new ClassworkSheetVM
            {
                Classwork = classwork,
                ClassworkSheet= classworksheet
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
            _userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
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