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
    public class SchoolNoticesController : Controller
    {
        private readonly ILogger<SchoolNoticesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public SchoolNoticesController(ILogger<SchoolNoticesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(long Id)
        {

            SchoolNotice subject = new SchoolNotice();

            //this is for create
            subject.SchoolID = Id;
            return View(subject);

            //this is for edit
            //subject = await _unitOfWork.Subjects.GetAsync(Id.GetValueOrDefault());
            //if (subject == null)
            //{
            //    return NotFound();
            //}
            //return View(subject);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;


                if (subject.SubjectID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    subject.CreatedBy = User.Identity.Name;
                    subject.CreatedDate = workdate;
                    subject.UpdatedBy = User.Identity.Name;
                    subject.UpdatedDate = workdate;
                    _unitOfWork.Subject.AddAsync(subject);

                }
                else
                {

                    subject.UpdatedBy = User.Identity.Name;
                    subject.UpdatedDate = workdate;

                    _unitOfWork.Subject.Update(subject);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Subject.GetAllAsync(c => c.School.OwnerId == _userId, includeProperties: "School");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Subject.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Subject.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}