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
    public class SchoolsController : Controller
    {
        private readonly ILogger<SchoolsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public SchoolsController(ILogger<SchoolsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.School.GetAllAsync(t => t.OwnerId == _userId);
            return View(allObj.Select(a => new SchoolVM { SchoolID = a.SchoolID, SchoolName = a.SchoolName, PhoneNumber = a.PhoneNumber, isauthorize = a.IsAuthorizedSchool == true ? "Authorized" : "Not Authorized" }));
        }

        public async Task<IActionResult> Upsert(long? Id)
        {

            School school = new School();
            if (Id == null)
            {
                //this is for create
                return View(school);
            }
            //this is for edit
            school = await _unitOfWork.School.GetAsync(Id.GetValueOrDefault());
            if (school == null)
            {
                return NotFound();
            }
            return View(school);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(School school)
        {
            if (ModelState.IsValid)
            {
                var workdate= DateTime.Now;


                if (school.SchoolID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    school.OwnerId = _userId;

                    school.CreatedBy = User.Identity.Name;
                    school.CreatedDate = workdate;
                    school.UpdatedBy = User.Identity.Name;
                    school.UpdatedDate = workdate;
                    _unitOfWork.School.AddAsync(school);

                }
                else
                {

                    school.UpdatedBy = User.Identity.Name;
                    school.UpdatedDate = workdate;

                    _unitOfWork.School.Update(school);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.School.GetAllAsync(t => t.OwnerId == _userId);
            return Json(new { data = allObj.Select(a => new { id = a.SchoolID, schoolname = a.SchoolName, phonenumber = a.PhoneNumber, isauthorize = a.IsAuthorizedSchool == true ? "Authorized" : "Not Authorized" }) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.School.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.School.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}