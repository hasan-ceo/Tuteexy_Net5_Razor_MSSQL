using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;

namespace Tuteexy.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = SD.Role_User)]
    public class UserProfilesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserProfilesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;


        public UserProfilesController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserProfilesController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            userprofile = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(t=>t.UserProfileID==Id.GetValueOrDefault(),includeProperties:"User");
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

        //public IActionResult Create()
        //{
        //   return View();
           
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(UserProfile tutorjob)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var workdate = DateTime.Now;


        //        if (tutorjob.UserProfileID == 0)
        //        {
        //            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        //            _unitOfWork.UserProfile.AddAsync(tutorjob);

        //        }
        //        else
        //        {

        //            _unitOfWork.UserProfile.Update(tutorjob);
        //        }

        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(tutorjob);
        //}

        //public async Task<IActionResult> Preview(long id)
        //{
        //    var t = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(h => h.UserProfileID == id);
        //    return View(t);
        //}

        public IActionResult ChangePassword(long Id)
        {
            ChangePasswordVM cp = new ChangePasswordVM
            {
                Id = Id
            };
            return View(cp);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword)
        {
            if (!ModelState.IsValid)
            {
                TempData["StatusMessage"] = $"Error : Please check input";
                return View(changePassword);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["StatusMessage"] = $"Error : Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return View(changePassword);
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["StatusMessage"] = $"Error : Unable to change your password. Try again.";
                return View(changePassword);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            TempData["StatusMessage"] = $"Your password has been changed.";
            return LocalRedirect("~/User/UserProfiles/Upsert/"+ changePassword.Id);
        }


        public IActionResult ChangeEmail(long Id)
        {
            ChangeEmailVM cp = new ChangeEmailVM
            {
                Id = Id
            };
            return View(cp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailVM changeEmail)
        {
            if (!ModelState.IsValid)
            {
                TempData["StatusMessage"] = $"Error : Please check input";
                return View(changeEmail);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["StatusMessage"] = $"Error : Unable to load user with ID '{_userManager.GetUserId(User)}'.";
                return View(changeEmail);
            }

            var code = await _userManager.GenerateChangeEmailTokenAsync(user, changeEmail.NewEmail);
            var changeEmailResult = await _userManager.ChangeEmailAsync(user, changeEmail.NewEmail, code);
            if (!changeEmailResult.Succeeded)
            {
                foreach (var error in changeEmailResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["StatusMessage"] = $"Error : Unable to change your email. Try again.";
                return View(changeEmail);
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their email successfully.");
            TempData["StatusMessage"] = $"Your email has been changed.";
            return LocalRedirect("~/User/UserProfiles/Upsert/" + changeEmail.Id);
        }
    }
}