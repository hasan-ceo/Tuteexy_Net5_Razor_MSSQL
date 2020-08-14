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

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
    [Authorize(Roles = SD.Role_User)]
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private string _userId;

        public QuestionsController(ILogger<QuestionsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var allObj = await _unitOfWork.Question.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View();
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            Question question = new Question();
            if (Id == null)
            {
                //this is for create
                return View(question);
            }
            //this is for edit
            question = await _unitOfWork.Question.GetAsync(Id.GetValueOrDefault());
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Question question)
        {
            if (ModelState.IsValid)
            {
                if (question.QuestionID == 0)
                {
                    question.SubmittedDate = DateTime.Now;
                    question.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.Question.AddAsync(question);
                }
                else
                {
                    var tmpQ = await _unitOfWork.Question.GetAsync(question.QuestionID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = question.Description;
                    tmpQ.IsReplyClose = question.IsReplyClose;
                    _unitOfWork.Question.Update(tmpQ);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        public async Task<IActionResult> Answer(long? Id)
        {
            var question = await _unitOfWork.Question.GetFirstOrDefaultAsync(q=>q.QuestionID==Id,includeProperties:"User");
            var questionthread = await _unitOfWork.QuestionThread.GetAllAsync(q => q.QuestionID == Id, includeProperties: "User");
            QuestionVM questionVM = new QuestionVM
            {
                Question = question,
                QuestionThread=questionthread.OrderByDescending(q=>q.SubmittedDate),
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            //var allObj = await _unitOfWork.Question.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(questionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(QuestionThread questionthread)
        {
            if (ModelState.IsValid)
            {
                if (questionthread.QuestionThreadID == 0)
                {
                    questionthread.SubmittedDate = DateTime.Now;
                    questionthread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.QuestionThread.AddAsync(questionthread);
                }
                else
                {
                    var tmpQ = await _unitOfWork.Question.GetAsync(questionthread.QuestionID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = questionthread.Description;
                    tmpQ.IsReplyClose = questionthread.IsReplyClose;
                    _unitOfWork.QuestionThread.Update(questionthread);
                }

                _unitOfWork.Save();
                //return RedirectToAction("Answer", questionthread.QuestionID);
            }
            return RedirectToAction("Answer", questionthread.QuestionID);
        }

        public IActionResult Qas(long Id)
        {
            return ViewComponent("QuestionA", new { id = Id });
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Question.GetAllAsync(c => c.UserID == _userId);
            return Json(new { data = allObj.Select(a => new { id = a.QuestionID, description = a.Description, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive = a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Question.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Question.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}