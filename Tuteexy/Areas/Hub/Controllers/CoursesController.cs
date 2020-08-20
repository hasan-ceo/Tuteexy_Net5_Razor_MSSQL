using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;

namespace Tuteexy.Areas.Hub.Controllers
{
    [Area("Hub")]
    [Authorize(Roles = SD.Role_User)]
    public class CoursesController : Controller
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;

        public CoursesController(ILogger<CoursesController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var allObj = await _unitOfWork.Course.GetAllAsync(includeProperties: "User");
            return View(allObj);
        }

        public async Task<IActionResult> MyCourses()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Course.GetAllAsync(c => c.UserID == _userId, includeProperties: "User");

            return View(allObj);
        }


        public async Task<IActionResult> Upsert(long? Id)
        {
            Course course = new Course();
            if (Id == null)
            {
                //this is for create
                return View(course);
            }
            //this is for edit
            course = await _unitOfWork.Course.GetAsync(Id.GetValueOrDefault());
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Course course)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\courses");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (course.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, course.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    course.ImageUrl = fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (course.CourseID != 0)
                    {
                        Course objFromDb = await _unitOfWork.Course.GetAsync(course.CourseID);
                        course.ImageUrl = objFromDb.ImageUrl;
                    }
                    else
                    {
                        course.ImageUrl = "coursedefaultimg.jpg";
                    }
                }

                if (course.CourseID == 0)
                {
                    course.SubmittedDate = DateTime.Now;
                    course.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.Course.AddAsync(course);
                }
                else
                {
                    var tmpQ = await _unitOfWork.Course.GetAsync(course.CourseID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Title = course.Title;
                    tmpQ.Description = course.Description;
                    tmpQ.IsReplyClose = course.IsReplyClose;
                    tmpQ.ImageUrl = course.ImageUrl;
                    _unitOfWork.Course.Update(tmpQ);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(MyCourses));
            }
            return View(course);
        }

        public async Task<IActionResult> Details(long? Id)
        {
            var question = await _unitOfWork.Course.GetFirstOrDefaultAsync(q => q.CourseID == Id, includeProperties: "User");
            var questionthread = await _unitOfWork.CourseThread.GetAllAsync(q => q.CourseID == Id, includeProperties: "User");
            CourseVM courseVM = new CourseVM
            {
                Course = question,
                CourseThread = questionthread,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
            };
            //var allObj = await _unitOfWork.Course.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(courseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(CourseThread coursethread)
        {
            if (ModelState.IsValid)
            {
                if (coursethread.CourseThreadID == 0)
                {
                    coursethread.SubmittedDate = DateTime.Now;
                    coursethread.UserID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    await _unitOfWork.CourseThread.AddAsync(coursethread);
                }
                else
                {
                    var tmpQ = await _unitOfWork.Course.GetAsync(coursethread.CourseID);
                    tmpQ.SubmittedDate = DateTime.Now;
                    tmpQ.Description = coursethread.Description;
                    tmpQ.IsReplyClose = coursethread.IsReplyClose;
                    _unitOfWork.CourseThread.Update(coursethread);
                }

                _unitOfWork.Save();
                //return RedirectToAction("Answer", questionthread.CourseID);
            }
            return RedirectToAction("Details", coursethread.CourseID);
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allObj = await _unitOfWork.Course.GetAllAsync();
            return Json(new { data = allObj.Select(a => new { id = a.CourseID, title = a.Title, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive = a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpGet]
        public async Task<IActionResult> GetMyStories()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var allObj = await _unitOfWork.Course.GetAllAsync(c => c.UserID == _userId);
            return Json(new { data = allObj.Select(a => new { id = a.CourseID, title = a.Title, isreplyclose = a.IsReplyClose, isapproved = a.IsApproved, isoffensive = a.IsOffensive, submitteddate = a.SubmittedDate.ToString("dd/MMM/yyyy") }) });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var objFromDb = await _unitOfWork.Course.GetAsync(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            await _unitOfWork.Course.RemoveEntityAsync(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion

    }
}