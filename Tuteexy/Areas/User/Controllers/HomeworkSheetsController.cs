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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Tuteexy.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class HomeworkSheetsController : Controller
    {
        private readonly ILogger<HomeworkSheetsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;

        [BindProperty]
        public HomeworkSheet hwreply { get; set; }

        public HomeworkSheetsController(ILogger<HomeworkSheetsController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Reply(long Id)
        {
            
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var hwr = await _unitOfWork.HomeworkSheet.GetFirstOrDefaultAsync(i => i.HomeworkID == Id && i.StudentID == _userId,includeProperties: "Homework");
            if (hwr == null)
            {
                HomeworkSheet hwreply = new HomeworkSheet();
                hwreply.HomeworkID = Id;
                hwreply.StudentID = _userId;
                hwreply.DateSubmitted = DateTime.Now;
                hwreply.Description = "";
                hwreply.AttachLink1 = "";
                hwreply.AttachLink2 = "";
                hwreply.AttachLink3 = "";
                hwreply.AttachLink4 = "";
                hwreply.AttachLink5 = "";
                hwreply.HwMarks = 0;
                hwreply.HWStatus = SD.StatusPending;

                await _unitOfWork.HomeworkSheet.AddAsync(hwreply);
                _unitOfWork.Save();

                var tmp = await _unitOfWork.Homework.GetFirstOrDefaultAsync(i => i.HomeworkID == hwreply.HomeworkID);
                hwreply.Homework = tmp;
                return View(hwreply);
            }
            //var allObj = await _unitOfWork.TutorJob.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(hwr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var hwr = await _unitOfWork.HomeworkSheet.GetFirstOrDefaultAsync(i => i.HomeworkSheetID == hwreply.HomeworkSheetID);
            if (hwr == null)
            {
                TempData["StatusMessage"] = $"Error : Please check homework";
                return LocalRedirect("/User/Dashboard/Homework");
            }

            hwr.Description = hwreply.Description;
            hwr.DateSubmitted = DateTime.Now;
            hwr.HWStatus =SD.StatusSubmitted;

            var uploads = Path.Combine(webRootPath, @"images\homeworks");

            if (hwr.AttachLink1 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink1.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink2 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink2.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink3 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink3.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink4 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink4.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink5 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink5.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            hwr.AttachLink1 = "";
            hwr.AttachLink2 = "";
            hwr.AttachLink3 = "";
            hwr.AttachLink4 = "";
            hwr.AttachLink5 = "";

            var files = HttpContext.Request.Form.Files;
            var i = files.Count;
            var j = 1;
            if (files.Count > 0)
            {
                
                if (j <= i)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[0].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    hwr.AttachLink1 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[1].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[1].CopyTo(filesStreams);
                    }
                    hwr.AttachLink2 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[2].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[2].CopyTo(filesStreams);
                    }
                    hwr.AttachLink3 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[3].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[3].CopyTo(filesStreams);
                    }
                    hwr.AttachLink4 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[4].CopyTo(filesStreams);
                    }
                    hwr.AttachLink5 = fileName + extenstion;
                    j++;
                }

            }


            if (hwr.HomeworkSheetID != 0)
            {
                _unitOfWork.HomeworkSheet.Update(hwr);
            }

            _unitOfWork.Save();
            return RedirectToAction("Reply", new { Id=hwr.HomeworkID});
        }

    }
}