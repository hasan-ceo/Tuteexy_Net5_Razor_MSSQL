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

namespace Titan.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class SubjectsController : Controller
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public SubjectsController(ILogger<SubjectsController> logger, IUnitOfWork unitOfWork)
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

            Subject subject = new Subject();

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
                    _unitOfWork.Subjects.AddAsync(subject);

                }
                else
                {

                    subject.UpdatedBy = User.Identity.Name;
                    subject.UpdatedDate = workdate;

                    _unitOfWork.Subjects.Update(subject);
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
            var allObj = await _unitOfWork.Subjects.GetAllAsync(c => c.School.OwnerId == _userId, includeProperties: "School");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Subjects.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Subjects.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}