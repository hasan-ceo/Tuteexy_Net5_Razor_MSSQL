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

namespace Tuteexy.Areas.Ironman.Controllers
{
    [Area("Ironman")]
    [Authorize(Roles = SD.Role_Ironman)]
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        //private string _userId;
        public PagesController(ILogger<PagesController> logger, IUnitOfWork unitOfWork)
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
            Page page = new Page();
            if (Id == null)
            {
                //this is for create
                return View(page);
            }
            //this is for edit
            page = await _unitOfWork.Page.GetAsync(Id.GetValueOrDefault());
            if (page == null)
            {
                return NotFound();
            }
            return View(page);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Page page)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;
                if (page.PageID == 0)
                {

                    page.CreatedBy = User.Identity.Name;
                    page.CreatedDate = workdate;
                    page.UpdatedBy = User.Identity.Name;
                    page.UpdatedDate = workdate;
                    _unitOfWork.Page.AddAsync(page);

                }
                else
                {
                    page.UpdatedBy = User.Identity.Name;
                    page.UpdatedDate = workdate;
                    _unitOfWork.Page.Update(page);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _unitOfWork.Page.GetAllAsync();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Page.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Page.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}