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
    public class HolidaysController : Controller
    {
        private readonly ILogger<HolidaysController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public HolidaysController(ILogger<HolidaysController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.School.GetFirstOrDefaultAsync(c => c.OwnerId == _userId);
            if (allObj != null)
                return View();
            else
                return LocalRedirect("/Hub/Dashboard/index");
        }

        public IActionResult Create(long Id)
        {
            var sn = new Holiday
            {
                SchoolID = Id,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now
            };


            //this is for create
            return View("Upsert", sn);
        }

        public async Task<IActionResult> Edit(long Id)
        {
            var holiday = await _unitOfWork.Holiday.GetAsync(Id);

            //this is for edit

            if (holiday == null)
            {
                return NotFound();
            }

            return View("Upsert", holiday);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (holiday.HolidayID == 0)
                {
                    holiday.CreatedBy = _userId;
                    holiday.CreatedDate = workdate;
                    holiday.UpdatedBy = _userId;
                    holiday.UpdatedDate = workdate;
                    holiday.Duration = (holiday.DateEnd - holiday.DateStart).Days + 1;
                    await _unitOfWork.Holiday.AddAsync(holiday);

                }
                else
                {
                    holiday.UpdatedBy = _userId;
                    holiday.UpdatedDate = workdate;
                    holiday.Duration = (holiday.DateEnd - holiday.DateStart).Days + 1;
                    _unitOfWork.Holiday.Update(holiday);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(holiday);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Holiday.GetAllAsync(c => c.School.OwnerId == _userId, includeProperties: "School");
            return Json(new { data = allObj.Select(a => new { id = a.HolidayID, schoolname = a.School.SchoolName, datestart = a.DateStart.ToString("dd/MMM/yyyy"), dateend = a.DateEnd.ToString("dd/MMM/yyyy"), holidayname = a.HolidayName, duration = a.Duration }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Holiday.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Holiday.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}