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

namespace Titan.Areas.Ironman.Controllers
{
    [Area("Ironman")]
    [Authorize(Roles = SD.Role_Ironman)]
    public class PagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {

            var allObj = await _unitOfWork.Pages.GetAllAsync();
            return View(allObj);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Page page = new Page();
            if (id == null)
            {
                //this is for create
                return View(page);
            }
            //this is for edit
            page = await _unitOfWork.Pages.GetAsync(id.GetValueOrDefault());
            if (page == null)
            {
                return NotFound();
            }
            return View(page);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Page page)
        {
            if (ModelState.IsValid)
            {
                if (page.PageID == 0)
                {
                    await _unitOfWork.Pages.AddAsync(page);

                }
                else
                {
                    _unitOfWork.Pages.Update(page);
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
            var allObj = await _unitOfWork.Pages.GetAllAsync();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var objFromDb = await _unitOfWork.Pages.GetAsync(id);
            if (objFromDb == null)
            {
                TempData["Error"] = "Error deleting Page";
                return Json(new { success = false, message = "Error while deleting" });
            }

            await _unitOfWork.Pages.RemoveAsync(objFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "Page successfully deleted";
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}