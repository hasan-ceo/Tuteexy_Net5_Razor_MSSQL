using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            ClassRoomNoticeVM classroomnoticeVM = new ClassRoomNoticeVM()
            {
                ClassRoomNotice = sn,
                ScheduleTime = DateTime.Now
            };

            //this is for create
            return View("Upsert", classroomnoticeVM);
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
                ScheduleTime = schoolnotice.ScheduleDateTime

            };
            return View("Upsert", schoolnoticeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ClassRoomNoticeVM classRoomNoticevm)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                classRoomNoticevm.ClassRoomNotice.ScheduleDateTime = classRoomNoticevm.ClassRoomNotice.ScheduleDateTime.Add(classRoomNoticevm.ScheduleTime.TimeOfDay);

                if (classRoomNoticevm.ClassRoomNotice.ClassRoomNoticeID == 0)
                {
                    classRoomNoticevm.ClassRoomNotice.CreatedBy = _userId;
                    classRoomNoticevm.ClassRoomNotice.CreatedDate = workdate;
                    classRoomNoticevm.ClassRoomNotice.UpdatedBy = _userId;
                    classRoomNoticevm.ClassRoomNotice.UpdatedDate = workdate;

                    await _unitOfWork.ClassRoomNotice.AddAsync(classRoomNoticevm.ClassRoomNotice);

                }
                else
                {
                    classRoomNoticevm.ClassRoomNotice.UpdatedBy = _userId;
                    classRoomNoticevm.ClassRoomNotice.UpdatedDate = workdate;
                    _unitOfWork.ClassRoomNotice.Update(classRoomNoticevm.ClassRoomNotice);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(classRoomNoticevm);
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