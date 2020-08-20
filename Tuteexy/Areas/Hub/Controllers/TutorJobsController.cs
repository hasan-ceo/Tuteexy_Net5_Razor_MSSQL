using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
    [Authorize(Roles = SD.Role_User)]
    public class TutorJobsController : Controller
    {
        private readonly ILogger<TutorJobsController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TutorJobsController(ILogger<TutorJobsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var allObj = await _unitOfWork.TutorJob.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View();
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            TutorJob tutorJob = new TutorJob();
            if (Id == null)
            {
                //this is for create
                return View(tutorJob);
            }
            //this is for edit
            tutorJob = await _unitOfWork.TutorJob.GetAsync(Id.GetValueOrDefault());
            if (tutorJob == null)
            {
                return NotFound();
            }
            return View(tutorJob);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TutorJob tutorJob)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;
                if (tutorJob.TutorJobID == 0)
                {
                    tutorJob.CreatedBy = User.Identity.Name;
                    tutorJob.CreatedDate = workdate;
                    tutorJob.UpdatedBy = User.Identity.Name;
                    tutorJob.UpdatedDate = workdate;
                    await _unitOfWork.TutorJob.AddAsync(tutorJob);
                }
                else
                {
                    tutorJob.UpdatedBy = User.Identity.Name;
                    tutorJob.UpdatedDate = workdate;
                    _unitOfWork.TutorJob.Update(tutorJob);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorJob);
        }

        //public IActionResult Create()
        //{
        //   return View();

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(TutorJob tutorjob)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var workdate = DateTime.Now;


        //        if (tutorjob.TutorJobID == 0)
        //        {
        //            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //            tutorjob.CreatedBy = User.Identity.Name;
        //            tutorjob.CreatedDate = workdate;
        //            tutorjob.UpdatedBy = User.Identity.Name;
        //            tutorjob.UpdatedDate = workdate;
        //            _unitOfWork.TutorJob.AddAsync(tutorjob);

        //        }
        //        else
        //        {

        //            tutorjob.UpdatedBy = User.Identity.Name;
        //            tutorjob.UpdatedDate = workdate;

        //            _unitOfWork.TutorJob.Update(tutorjob);
        //        }

        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tutorjob);
        //}

        public async Task<IActionResult> Preview(long id)
        {
            var t = await _unitOfWork.TutorJob.GetFirstOrDefaultAsync(h => h.TutorJobID == id);
            return View(t);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _unitOfWork.TutorJob.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return Json(new { data = allObj.Select(a => new { id = a.TutorJobID, jobtitle = a.JobTitle, course = a.Course, subject = a.Subject, salary = a.Salary }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.TutorJob.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.TutorJob.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}