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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
    [Authorize(Roles = SD.Role_User)]
    public class ShortStoriesController : Controller
    {
        private readonly ILogger<ShortStoriesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;

        public ShortStoriesController(ILogger<ShortStoriesController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var allObj = await _unitOfWork.ShortStory.GetAllAsync(includeProperties:"User");
            return View(allObj);
        }

        public async Task<IActionResult> MyStories()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ShortStory.GetAllAsync(c => c.UserID == _userId, includeProperties: "User");

            return View(allObj);
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            ShortStory shortstory = new ShortStory();
            if (Id == null)
            {
                //this is for create
                return View(shortstory);
            }
            //this is for edit
            shortstory = await _unitOfWork.ShortStory.GetAsync(Id.GetValueOrDefault());
            if (shortstory == null)
            {
                return NotFound();
            }
            return View(shortstory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ShortStory shortstory)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\stories");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (shortstory.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, shortstory.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    shortstory.ImageUrl = fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (shortstory.ShortStoryID != 0)
                    {
                        ShortStory objFromDb = await _unitOfWork.ShortStory.GetAsync(shortstory.ShortStoryID);
                        shortstory.ImageUrl = objFromDb.ImageUrl;
                    }
                    else {
                        shortstory.ImageUrl = "storydefaultimg.jpg";
                    }
                }

                if (shortstory.ShortStoryID == 0)
                {
                    shortstory.SubmittedDate = DateTime.Now;
                    shortstory.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.ShortStory.AddAsync(shortstory);
                }
                else
                {
                    var tmpQ = await _unitOfWork.ShortStory.GetAsync(shortstory.ShortStoryID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Title = shortstory.Title;
                    tmpQ.Description = shortstory.Description;
                    tmpQ.IsReplyClose = shortstory.IsReplyClose;
                    tmpQ.ImageUrl = shortstory.ImageUrl;
                    _unitOfWork.ShortStory.Update(tmpQ);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(MyStories));
            }
            return View(shortstory);
        }

        public async Task<IActionResult> Details(long? Id)
        {
            var question = await _unitOfWork.ShortStory.GetFirstOrDefaultAsync(q=>q.ShortStoryID==Id,includeProperties:"User");
            var questionthread = await _unitOfWork.ShortStoryThread.GetAllAsync(q => q.ShortStoryID == Id, includeProperties: "User");
            ShortStoryVM shortstoryVM = new ShortStoryVM
            {
                ShortStory = question,
                ShortStoryThread=questionthread,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            //var allObj = await _unitOfWork.ShortStory.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(shortstoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShortStoryThread shortstorythread)
        {
            if (ModelState.IsValid)
            {
                if (shortstorythread.ShortStoryThreadID == 0)
                {
                    shortstorythread.SubmittedDate = DateTime.Now;
                    shortstorythread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.ShortStoryThread.AddAsync(shortstorythread);
                }
                else
                {
                    var tmpQ = await _unitOfWork.ShortStory.GetAsync(shortstorythread.ShortStoryID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = shortstorythread.Description;
                    tmpQ.IsReplyClose = shortstorythread.IsReplyClose;
                    _unitOfWork.ShortStoryThread.Update(shortstorythread);
                }

                _unitOfWork.Save();
                //return RedirectToAction("Answer", questionthread.ShortStoryID);
            }
            return RedirectToAction("Details", shortstorythread.ShortStoryID);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _unitOfWork.ShortStory.GetAllAsync();
            return Json(new { data = allObj.Select(a => new { id = a.ShortStoryID, title = a.Title, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive = a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpGet]
        public async Task<IActionResult> GetMyStories()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ShortStory.GetAllAsync(c => c.UserID == _userId);
            return Json(new { data = allObj.Select(a => new { id = a.ShortStoryID, title = a.Title, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive = a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.ShortStory.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.ShortStory.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}