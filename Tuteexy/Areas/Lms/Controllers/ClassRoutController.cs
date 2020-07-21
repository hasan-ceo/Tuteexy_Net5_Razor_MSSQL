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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class ClassRoutController : Controller
    {
        private readonly ILogger<ClassRoutController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;


        public ClassRoutController(ILogger<ClassRoutController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(long? Id)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var y = await _unitOfWork.SchoolTeacher.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
            IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => c.SchoolID == y.SchoolID);

            var sbl = SubList.Select(i => new SelectListItem
            {
                Text = i.SubjectName,
                Value = i.SubjectName
            });
            ClassRoutineVM classroutineVM = new ClassRoutineVM()
            {
                ClassRoutine = new ClassRoutine(),
                ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                }),
                DayList = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Sunday", Value = "Sunday"},
                    new SelectListItem {Text = "Monday", Value = "Monday"},
                    new SelectListItem {Text = "Tuesday", Value = "Tuesday"},
                    new SelectListItem {Text = "Wednesday", Value = "Wednesday"},
                    new SelectListItem {Text = "Thursday", Value = "Thursday"},
                    new SelectListItem {Text = "Friday", Value = "Friday"},
                     new SelectListItem {Text = "Saturday", Value = "Saturday"}
                },
                P1List = sbl,
                P2List = sbl,
                P3List = sbl,
                P4List = sbl,
                P5List = sbl,
                P6List = sbl,
                P7List = sbl,
                P8List = sbl,
                P9List = sbl,
                P10List = sbl,
            };
            if (Id == null)
            {
                //this is for create
                return View(classroutineVM);
            }
            //this is for edit
            classroutineVM.ClassRoutine = await _unitOfWork.ClassRoutine.GetAsync(Id.GetValueOrDefault());
            if (classroutineVM.ClassRoutine == null)
            {
                return NotFound();
            }
            return View(classroutineVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ClassRoutineVM classroutineVM)
        {
            if (ModelState.IsValid)
            {
                if (classroutineVM.ClassRoutine.ClassRoutineID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    classroutineVM.ClassRoutine.CreatedBy = _userId;
                    classroutineVM.ClassRoutine.CreatedDate = DateTime.Now;
                    classroutineVM.ClassRoutine.UpdatedBy = _userId;
                    classroutineVM.ClassRoutine.UpdatedDate = DateTime.Now;
                    await _unitOfWork.ClassRoutine.AddAsync(classroutineVM.ClassRoutine);
                }
                else
                {
                    classroutineVM.ClassRoutine.UpdatedBy = _userId;
                    classroutineVM.ClassRoutine.UpdatedDate = DateTime.Now;
                    _unitOfWork.ClassRoutine.Update(classroutineVM.ClassRoutine);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var y = await _unitOfWork.SchoolTeacher.GetFirstOrDefaultAsync(t => t.TeacherID == _userId);
                IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
                IEnumerable<Subject> SubList = await _unitOfWork.Subject.GetAllAsync(c => c.SchoolID == y.SchoolID);

                classroutineVM.ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                });
                classroutineVM.DayList = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Sunday", Value = "Sunday"},
                    new SelectListItem {Text = "Monday", Value = "Monday"},
                    new SelectListItem {Text = "Tuesday", Value = "Tuesday"},
                    new SelectListItem {Text = "Wednesday", Value = "Wednesday"},
                    new SelectListItem {Text = "Thursday", Value = "Thursday"},
                    new SelectListItem {Text = "Friday", Value = "Friday"},
                     new SelectListItem {Text = "Saturday", Value = "Saturday"}
                };
                var sbl = SubList.Select(i => new SelectListItem
                {
                    Text = i.SubjectName,
                    Value = i.SubjectName
                });
                classroutineVM.P1List = sbl;
                classroutineVM.P2List = sbl;
                classroutineVM.P3List = sbl;
                classroutineVM.P4List = sbl;
                classroutineVM.P5List = sbl;
                classroutineVM.P6List = sbl;
                classroutineVM.P7List = sbl;
                classroutineVM.P8List = sbl;
                classroutineVM.P9List = sbl;
                classroutineVM.P10List = sbl;

                if (classroutineVM.ClassRoutine.ClassRoutineID != 0)
                {
                    classroutineVM.ClassRoutine = await _unitOfWork.ClassRoutine.GetAsync(classroutineVM.ClassRoutine.ClassRoutineID);
                }
            }
            return View(classroutineVM);
        }
        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // "182596ba-2fcc-4db7-8053-395e1af1a276";//
            var allObj = await _unitOfWork.ClassRoutine.GetAllAsync(t => t.ClassRoom.School.OwnerId == _userId, includeProperties: "ClassRoom");
            return Json(new
            {
                data = allObj.Select(o => new {
                    id = o.ClassRoutineID,
                    classname = o.ClassRoom.ClassRoomName,
                    day = o.DayName,
                    p1 = o.Period1,
                    p2 = o.Period2,
                    p3 = o.Period4,
                    p4 = o.Period4,
                    p5 = o.Period5,
                    p6 = o.Period6,
                    p7 = o.Period7,
                    p8 = o.Period8,
                    p9 = o.Period9,
                    p10 = o.Period10
                })
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ClassRoutine.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ClassRoutine.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}