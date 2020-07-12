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

        public async Task<IActionResult> Index()
        {

            var allObj = await _unitOfWork.Pages.GetAllAsync();
            return View(allObj);
        }

        public async Task<IActionResult> Upsert(long? id)
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
        public IActionResult Upsert(Page page)
        {
            if (ModelState.IsValid)
            {
                if (page.PageID == 0)
                {
                    _unitOfWork.Pages.AddAsync(page);

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

    }
}