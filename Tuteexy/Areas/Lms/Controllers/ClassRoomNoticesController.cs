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
    //[Route("api/[controller]")]
    //[ApiController]
    public class ClassRoomNoticesController : Controller
    {
        private readonly ILogger<ClassRoomNoticesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassRoomNoticesController(ILogger<ClassRoomNoticesController> logger, IUnitOfWork unitOfWork)
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
            var sn = new ClassRoomNotice
            {
                ClassRoomID = Id,
                ScheduleDateTime = DateTime.Now
            };

            ClassRoomNoticeVM schoolnoticeVM = new ClassRoomNoticeVM()
            {
                ClassRoomNotice = sn,
                ScheduleTime = DateTime.Now.TimeOfDay.ToString()
            };

            //this is for create
            return View("Upsert", schoolnoticeVM);
        }

        public async Task<IActionResult> Edit(long Id)
        {
            var schoolnotice = await _unitOfWork.ClassRoomNotice.GetAsync(Id);

            //this is for edit

            if (schoolnotice == null)
            {
                return NotFound();
            }
            ClassRoomNoticeVM schoolnoticeVM = new ClassRoomNoticeVM()
            {
                ClassRoomNotice = schoolnotice,
                ScheduleTime = schoolnotice.ScheduleDateTime.TimeOfDay.ToString()
            };
            return View("Upsert", schoolnoticeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ClassRoomNoticeVM schoolnoticevm)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                TimeSpan ts = TimeSpan.Parse(schoolnoticevm.ScheduleTime);
                schoolnoticevm.ClassRoomNotice.ScheduleDateTime = schoolnoticevm.ClassRoomNotice.ScheduleDateTime.Add(ts);

                if (schoolnoticevm.ClassRoomNotice.ClassRoomNoticeID == 0)
                {
                    schoolnoticevm.ClassRoomNotice.CreatedBy = _userId;
                    schoolnoticevm.ClassRoomNotice.CreatedDate = workdate;
                    schoolnoticevm.ClassRoomNotice.UpdatedBy = _userId;
                    schoolnoticevm.ClassRoomNotice.UpdatedDate = workdate;

                    await _unitOfWork.ClassRoomNotice.AddAsync(schoolnoticevm.ClassRoomNotice);

                }
                else
                {
                    schoolnoticevm.ClassRoomNotice.UpdatedBy = _userId;
                    schoolnoticevm.ClassRoomNotice.UpdatedDate = workdate;
                    _unitOfWork.ClassRoomNotice.Update(schoolnoticevm.ClassRoomNotice);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolnoticevm.ClassRoomNotice);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ClassRoomNotice.GetAllAsync(c => c.ClassRoom.School.OwnerId == _userId, includeProperties: "ClassRoom");
            return Json(new { data = allObj.Select(a => new { id = a.ClassRoomNoticeID, classroomname = a.ClassRoom.ClassRoomName, title = a.Title, scheduledate = a.ScheduleDateTime.ToString("dd/MMM/yyyy hh:mm tt") }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ClassRoomNotice.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ClassRoomNotice.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}