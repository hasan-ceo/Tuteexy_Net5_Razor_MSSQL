using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class ClassRoomsController : Controller
    {
        private readonly ILogger<ClassRoomsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassRoomsController(ILogger<ClassRoomsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ClassRoom.GetAllAsync(c => c.School.OwnerId == _userId, includeProperties: "School");
            return View(allObj);
        }

        public IActionResult Create(long Id)
        {

            ClassRoom classRoom = new ClassRoom
            {
                SchoolID = Id
            };

            //this is for create
            return View(classRoom);

            //this is for edit
            //classRoom = await _unitOfWork.ClassRooms.GetAsync(Id.GetValueOrDefault());
            //if (classRoom == null)
            //{
            //    return NotFound();
            //}
            //return View(classRoom);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassRoom classRoom)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;


                if (classRoom.ClassRoomID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    classRoom.CreatedBy = User.Identity.Name;
                    classRoom.CreatedDate = workdate;
                    classRoom.UpdatedBy = User.Identity.Name;
                    classRoom.UpdatedDate = workdate;
                    _unitOfWork.ClassRoom.AddAsync(classRoom);

                }
                else
                {

                    classRoom.UpdatedBy = User.Identity.Name;
                    classRoom.UpdatedDate = workdate;

                    _unitOfWork.ClassRoom.Update(classRoom);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(classRoom);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ClassRoom.GetAllAsync(c => c.School.OwnerId == _userId, includeProperties: "School");
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ClassRoom.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ClassRoom.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}