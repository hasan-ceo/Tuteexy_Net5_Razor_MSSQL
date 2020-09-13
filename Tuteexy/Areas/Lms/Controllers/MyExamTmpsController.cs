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
using Tuteexy.Utility;

namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class MyExamTmpsController : Controller
    {
        private readonly ILogger<MyExamTmpsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;

        [BindProperty]
        public ExamTmpSheet hwreply { get; set; }

        public MyExamTmpsController(ILogger<MyExamTmpsController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> ExamTmp()
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var classroom = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == _userId);
            long classroomID = 0;
            if (classroom != null)
            {
                classroomID = classroom.ClassRoomID;
            }
            var allObj = await _unitOfWork.ExamTmp.GetAllAsync(h => h.ClassRoomID == classroomID && h.ScheduleDateTime <= DateTime.Now, h => h.OrderByDescending(p => p.DateDue), includeProperties: "ClassRoom,Teacher");
            // return View(allObj.Select(a => new { Title=a.Title, ExamTmpID=a.ExamTmpID, TeacherName = a.TeacherName, Subject= a.Subject }));
            return View(allObj.OrderByDescending(a => a.ExamTmpID));

        }

        public async Task<IActionResult> ExamTmpSheet(long Id)
        {

            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var hwr = await _unitOfWork.ExamTmpSheet.GetFirstOrDefaultAsync(i => i.ExamTmpID == Id && i.StudentID == _userId, includeProperties: "ExamTmp");
            if (hwr == null)
            {
                ExamTmpSheet hwreply = new ExamTmpSheet();
                hwreply.ExamTmpID = Id;
                hwreply.StudentID = _userId;
                hwreply.DateSubmitted = DateTime.Now;
                hwreply.Description = "";
                hwreply.AttachLink1 = "";
                hwreply.AttachLink2 = "";
                hwreply.AttachLink3 = "";
                hwreply.AttachLink4 = "";
                hwreply.AttachLink5 = "";
                hwreply.AttachLink6 = "";
                hwreply.AttachLink7 = "";
                hwreply.AttachLink8 = "";
                hwreply.AttachLink9 = "";
                hwreply.AttachLink10 = "";
                hwreply.AttachLink11 = "";
                hwreply.AttachLink12 = "";
                hwreply.AttachLink13 = "";
                hwreply.AttachLink14 = "";
                hwreply.AttachLink15 = "";
                hwreply.ExmMarks = 0;
                hwreply.ExmComments = "";
                hwreply.ExmStatus = SD.StatusPending;

                await _unitOfWork.ExamTmpSheet.AddAsync(hwreply);
                _unitOfWork.Save();

                var tmp = await _unitOfWork.ExamTmp.GetFirstOrDefaultAsync(i => i.ExamTmpID == hwreply.ExamTmpID);
                hwreply.ExamTmp = tmp;
                return View(hwreply);
            }
            //var allObj = await _unitOfWork.TutorJob.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(hwr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamTmpSheet()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var hwr = await _unitOfWork.ExamTmpSheet.GetFirstOrDefaultAsync(i => i.ExamTmpSheetID == hwreply.ExamTmpSheetID);
            if (hwr == null)
            {
                TempData["StatusMessage"] = $"Error : Please check examtmp";
                return RedirectToAction("ExamTmp");
            }

            hwr.Description = hwreply.Description;
            hwr.DateSubmitted = DateTime.Now;
            hwr.ExmStatus = SD.StatusSubmitted;

            var uploads = Path.Combine(webRootPath, @"images\examtmps");

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

            if (hwr.AttachLink6 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink6.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink7 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink7.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink8 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink8.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink9 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink9.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink10 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink10.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink11 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink11.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink12 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink12.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink13 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink13.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            if (hwr.AttachLink14 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink14.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }


            if (hwr.AttachLink15 != null)
            {
                //this is an edit and we need to remove old image
                var imagePath = Path.Combine(uploads, hwr.AttachLink15.TrimStart('\\'));
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
            hwr.AttachLink6 = "";
            hwr.AttachLink7 = "";
            hwr.AttachLink8 = "";
            hwr.AttachLink9 = "";
            hwr.AttachLink10 = "";
            hwr.AttachLink11 = "";
            hwr.AttachLink12 = "";
            hwr.AttachLink13 = "";
            hwr.AttachLink14 = "";
            hwr.AttachLink15 = "";

            var files = HttpContext.Request.Form.Files;
            var i = files.Count;
            var j = 1;
            if (files.Count > 0 && files.Count<=15)
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

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[5].CopyTo(filesStreams);
                    }
                    hwr.AttachLink6 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[6].CopyTo(filesStreams);
                    }
                    hwr.AttachLink7 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[7].CopyTo(filesStreams);
                    }
                    hwr.AttachLink8 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[8].CopyTo(filesStreams);
                    }
                    hwr.AttachLink9 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[9].CopyTo(filesStreams);
                    }
                    hwr.AttachLink10 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[10].CopyTo(filesStreams);
                    }
                    hwr.AttachLink11 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[11].CopyTo(filesStreams);
                    }
                    hwr.AttachLink12 = fileName + extenstion;
                    j++;
                }


                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[12].CopyTo(filesStreams);
                    }
                    hwr.AttachLink13 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[13].CopyTo(filesStreams);
                    }
                    hwr.AttachLink14 = fileName + extenstion;
                    j++;
                }

                if (j <= i)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var extenstion = Path.GetExtension(files[4].FileName);
                    var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create);
                    using (filesStreams)
                    {
                        files[14].CopyTo(filesStreams);
                    }
                    hwr.AttachLink15 = fileName + extenstion;
                    j++;
                }


            }


            if (hwr.ExamTmpSheetID != 0)
            {
                _unitOfWork.ExamTmpSheet.Update(hwr);
            }

            _unitOfWork.Save();
            return RedirectToAction("ExamTmpSheet", new { Id = hwr.ExamTmpID });
        }

    }
}