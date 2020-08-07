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
                hwreply.HWStatus = "Pendin";

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
        public async Task<IActionResult> Reply(HomeworkSheet hwreply)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var hwr = await _unitOfWork.HomeworkSheet.GetFirstOrDefaultAsync(i => i.HomeworkID == hwreply.HomeworkID && i.StudentID == _userId);
            if (hwr != null)
            {
                hwreply.AttachLink1 = string.IsNullOrEmpty(hwr.AttachLink1) == true ? "" : hwr.AttachLink1;
                hwreply.AttachLink2 = string.IsNullOrEmpty(hwr.AttachLink2) == true ? "" : hwr.AttachLink2;
                hwreply.AttachLink3 = string.IsNullOrEmpty(hwr.AttachLink3) == true ? "" : hwr.AttachLink3;
                hwreply.AttachLink4 = string.IsNullOrEmpty(hwr.AttachLink4) == true ? "" : hwr.AttachLink4;
                hwreply.AttachLink5 = string.IsNullOrEmpty(hwr.AttachLink5) == true ? "" : hwr.AttachLink5;
                hwreply.HomeworkSheetID = hwr.HomeworkSheetID;

            }
            else
            {
                hwreply.AttachLink1 = "";
                hwreply.AttachLink2 = "";
                hwreply.AttachLink3 = "";
                hwreply.AttachLink4 = "";
                hwreply.AttachLink5 = "";
            }
            hwreply.StudentID = _userId;
            hwreply.DateSubmitted = DateTime.Now;
            hwreply.Description = string.IsNullOrEmpty(hwreply.Description) == true ? "" : hwreply.Description;


            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                
                var uploads = Path.Combine(webRootPath, @"images\homeworks");

                if (files.Count ==1)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[0].FileName);
                    if (hwreply.AttachLink1 != "")
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, hwreply.AttachLink1.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    hwreply.AttachLink1 = fileName + extenstion;
                }

                if (files.Count == 2)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[1].FileName);
                    if (hwreply.AttachLink2 != "")
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, hwreply.AttachLink2.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[1].CopyTo(filesStreams);
                    }
                    hwreply.AttachLink2 = fileName + extenstion;
                }

                if (files.Count == 3)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[2].FileName);
                    if (hwreply.AttachLink3 != "")
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, hwreply.AttachLink3.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[2].CopyTo(filesStreams);
                    }
                    hwreply.AttachLink3 = fileName + extenstion;
                }

                if (files.Count == 4)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[3].FileName);
                    if (hwreply.AttachLink4 != "")
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, hwreply.AttachLink4.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[3].CopyTo(filesStreams);
                    }
                    hwreply.AttachLink4 = fileName + extenstion;
                }

                if (files.Count == 5)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    if (hwreply.AttachLink5 != "")
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(uploads, hwreply.AttachLink5.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[4].CopyTo(filesStreams);
                    }
                    hwreply.AttachLink5 = fileName + extenstion;
                }
            }


            if (hwreply.HomeworkSheetID == 0)
            {
                await _unitOfWork.HomeworkSheet.AddAsync(hwreply);
            }
            else
            {
                _unitOfWork.HomeworkSheet.Update(hwreply);
            }

            _unitOfWork.Save();
            return LocalRedirect("/User/Dashboard/Index");
        }

    }
}