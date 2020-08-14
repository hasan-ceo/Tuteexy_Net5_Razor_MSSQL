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

namespace Tuteexy.Areas.Ironman.Controllers
{
    [Area("Ironman")]
    [Authorize(Roles = SD.Role_Ironman)]
    public class ShortStoriesController : Controller
    {
        private readonly ILogger<ShortStoriesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;

        public ShortStoriesController(ILogger<ShortStoriesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var allObj = await _unitOfWork.ShortStory.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View();
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            ShortStory question = new ShortStory();
            if (Id == null)
            {
                //this is for create
                return View(question);
            }
            //this is for edit
            question = await _unitOfWork.ShortStory.GetAsync(Id.GetValueOrDefault());
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ShortStory question)
        {
            if (ModelState.IsValid)
            {
                if (question.ShortStoryID == 0)
                {
                    question.SubmittedDate = DateTime.Now;
                    question.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.ShortStory.AddAsync(question);
                }
                else
                {
                    var tmpQ = await _unitOfWork.ShortStory.GetAsync(question.ShortStoryID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = question.Description;
                    tmpQ.IsReplyClose = question.IsReplyClose;
                    _unitOfWork.ShortStory.Update(tmpQ);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        public async Task<IActionResult> Answer(long? Id)
        {
            var shortstory = await _unitOfWork.ShortStory.GetFirstOrDefaultAsync(q => q.ShortStoryID == Id, includeProperties: "User");
            var shortstorythread = await _unitOfWork.ShortStoryThread.GetAllAsync(q => q.ShortStoryID == Id, includeProperties: "User");
            ShortStoryVM shortstoryVM = new ShortStoryVM
            {
                ShortStory = shortstory,
                ShortStoryThread = shortstorythread.OrderByDescending(q=>q.ShortStoryThreadID)
            };
            //var allObj = await _unitOfWork.ShortStory.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(shortstoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(ShortStoryThread shortstorythread)
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
            return RedirectToAction("Answer", shortstorythread.ShortStoryID);
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.ShortStory.GetAllAsync(includeProperties:"User");
            return Json(new { data = allObj.Select(a => new { id = a.ShortStoryID, Title = a.Title, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive=a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }).OrderByDescending(a=>a.id) });

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

        [HttpPost]
        public async Task<IActionResult> Approved(long id)
        {
            var message = "Approved Successful";
            var objFromDb = await _unitOfWork.ShortStory.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while approve" });
            }

            if (objFromDb.IsApproved== true)
            {
                objFromDb.IsApproved = false;
                message = "Abort approve Successful";
            }
            else
            {
                objFromDb.IsApproved = true;
            }

            _unitOfWork.ShortStory.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = message });

        }


        [HttpPost]
        public async Task<IActionResult> Offensive(long id)
        {
            var message = "Offensive Post";
            var objFromDb = await _unitOfWork.ShortStory.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while mark as offensive" });
            }

            if (objFromDb.IsOffensive == true)
            {
                objFromDb.IsOffensive = false;
                message = "Not Offensive Post";
            }
            else
            {
                objFromDb.IsOffensive = true;
            }

            _unitOfWork.ShortStory.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = message });

        }


        [HttpPost]
        public async Task<IActionResult> ReplyClose(long id)
        {
            var message = "Reply Close Successful";
            var objFromDb = await _unitOfWork.ShortStory.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Reply Close" });
            }

            if (objFromDb.IsReplyClose == true)
            {
                objFromDb.IsReplyClose = false;
                message = "Abort Reply Close Successful";
            }
            else
            {
                objFromDb.IsReplyClose = true;
            }

            _unitOfWork.ShortStory.Update(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = message });

        }

        #endregion

    }
}