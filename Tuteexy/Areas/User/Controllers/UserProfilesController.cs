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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tuteexy.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class UserProfilesController : Controller
    {
        private readonly ILogger<UserProfilesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;


        public UserProfilesController(ILogger<UserProfilesController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Upsert(long? Id)
        {
            UserProfile userprofile = new UserProfile();
            if (Id == null)
            {
                //this is for create
                return View(userprofile);
            }
            //this is for edit
            userprofile = await _unitOfWork.UserProfile.GetAsync(Id.GetValueOrDefault());
            if (userprofile == null)
            {
                return NotFound();
            }
            return View(userprofile);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\students");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (userprofile.ImageUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, userprofile.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    userprofile.ImageUrl = fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (userprofile.UserProfileID != 0)
                    {
                        UserProfile objFromDb =await _unitOfWork.UserProfile.GetAsync(userprofile.UserProfileID);
                        userprofile.ImageUrl = objFromDb.ImageUrl;
                    }
                }


                if (userprofile.UserProfileID == 0)
                {
                    await _unitOfWork.UserProfile.AddAsync(userprofile);
                }
                else
                {
                    _unitOfWork.UserProfile.Update(userprofile);
                }

                _unitOfWork.Save();
                return LocalRedirect("/User/Dashboard/Index");
            }
            return View(userprofile);
        }

        public IActionResult Create()
        {
           return View();
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserProfile tutorjob)
        {
            if (ModelState.IsValid)
            {
                var workdate = DateTime.Now;


                if (tutorjob.UserProfileID == 0)
                {
                    _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    _unitOfWork.UserProfile.AddAsync(tutorjob);

                }
                else
                {

                    _unitOfWork.UserProfile.Update(tutorjob);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorjob);
        }

        public async Task<IActionResult> Preview(long id)
        {
            var t = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(h => h.UserProfileID == id);
            return View(t);
        }

    }
}