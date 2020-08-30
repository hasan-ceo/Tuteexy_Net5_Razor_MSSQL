using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using Tuteexy.Models.ViewModels;
using Tuteexy.Utility;


namespace Tuteexy.Areas.Lms.Controllers
{
    [Area("Lms")]
    [Authorize(Roles = SD.Role_User)]
    public class UserCreatesController : Controller
    {
        private readonly ILogger<UserCreatesController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string _userId;


        public UserCreatesController(UserManager<IdentityUser> userManager, IEmailSender emailSender, ILogger<UserCreatesController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult TeacherAdd(long Id)
        {

            UserCreateVM usercreatevm = new UserCreateVM
            {
                SchoolID = Id
            };

            return View(usercreatevm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeacherAdd(UserCreateVM Input, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                    Role = SD.Role_User
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, SD.Role_User);
                    _logger.LogInformation("Set Role to User");

                    await _userManager.AddClaimAsync(user, new Claim("InstituteID", Guid.NewGuid().ToString()));
                    _logger.LogInformation("Create a InstituteName");

                    var st = new SchoolTeacher
                    {
                        SchoolID = Input.SchoolID,
                        TeacherID = user.Id,
                        ApprovedBy = User.Identity.Name,
                        ApprovedDate = DateTime.Now,
                        IsApproved = true
                    };
                    await _unitOfWork.SchoolTeacher.AddAsync(st);
                    _unitOfWork.Save();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    var PathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                        + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates"
                        + Path.DirectorySeparatorChar.ToString() + "Confirm_Account_Registration.html";

                    var subject = "Confirm Account Registration";
                    string HtmlBody = "";
                    using (StreamReader streamReader = System.IO.File.OpenText(PathToFile))
                    {
                        HtmlBody = streamReader.ReadToEnd();
                    }

                    //{0} : Subject  
                    //{1} : DateTime  
                    //{2} : Name  
                    //{3} : Email  
                    //{4} : Message  
                    //{5} : callbackURL  

                    string Message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    string messageBody = string.Format(HtmlBody,
                        subject,
                        String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                        user.FullName,
                        user.Email,
                        Message,
                        callbackUrl
                        );


                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", messageBody);

                    //Input.StatusMessage = "Teacher add to school successfully";
                    //Input.Name = "";
                    //Input.PhoneNumber = "";
                    //Input.Email = "";
                    //Input.Password = "";
                    //Input.ConfirmPassword = "";
                    return LocalRedirect("/Lms/Schools/Index");
                }
                Input.StatusMessage = "Error : Can not create teacher, please try again.";
                return View(Input);
            }
            Input.StatusMessage = "Error : Invalid input data.";
            return View(Input);
        }

        public async Task<IActionResult> StudentAdd(long Id)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var y = await _unitOfWork.School.GetFirstOrDefaultAsync(t => t.OwnerId == _userId && t.SchoolID == Id);
            if (y == null)
            {
                TempData["StatusMessage"] = $"Error : You are not authorized to add student.";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/Homeworks/Index");
            }
            UserCreateVM usercreatevm = new UserCreateVM
            {
                ClassRoomList = clsList.Select(i => new SelectListItem
                {
                    Text = i.ClassRoomName,
                    Value = i.ClassRoomID.ToString()
                }),
                SchoolID = Id
            };

            return View(usercreatevm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentAdd(UserCreateVM Input, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                    Role = SD.Role_User
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, SD.Role_User);
                    _logger.LogInformation("Set Role to User");

                    await _userManager.AddClaimAsync(user, new Claim("InstituteID", Guid.NewGuid().ToString()));
                    _logger.LogInformation("Create a InstituteName");

                    var st = new ClassRoomStudent
                    {
                        ClassRoomID = Input.ClassRoomID,
                        StudentID = user.Id,
                        ApprovedBy = User.Identity.Name,
                        ApprovedDate = DateTime.Now,
                        IsApproved = true
                    };
                    await _unitOfWork.ClassRoomStudent.AddAsync(st);
                    _unitOfWork.Save();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);


                    var PathToFile = _hostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                        + "Templates" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates"
                        + Path.DirectorySeparatorChar.ToString() + "Confirm_Account_Registration.html";

                    var subject = "Confirm Account Registration";
                    string HtmlBody = "";
                    using (StreamReader streamReader = System.IO.File.OpenText(PathToFile))
                    {
                        HtmlBody = streamReader.ReadToEnd();
                    }

                    //{0} : Subject  
                    //{1} : DateTime  
                    //{2} : Name  
                    //{3} : Email  
                    //{4} : Message  
                    //{5} : callbackURL  

                    string Message = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    string messageBody = string.Format(HtmlBody,
                        subject,
                        String.Format("{0:dddd, d MMMM yyyy}", DateTime.Now),
                        user.FullName,
                        user.Email,
                        Message,
                        callbackUrl
                        );


                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email", messageBody);

                    //Input.StatusMessage = "Teacher add to school successfully";
                    //Input.Name = "";
                    //Input.PhoneNumber = "";
                    //Input.Email = "";
                    //Input.Password = "";
                    //Input.ConfirmPassword = "";
                    return LocalRedirect("/Lms/Schools/Index");
                }
                Input.StatusMessage = "Error : Can not create teacher, please try again.";
                return View(Input);
            }

            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var y = await _unitOfWork.School.GetFirstOrDefaultAsync(t => t.OwnerId == _userId && t.SchoolID == Input.SchoolID);
            if (y == null)
            {
                TempData["StatusMessage"] = $"Error : You are not authorized to add student.";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            IEnumerable<ClassRoom> clsList = await _unitOfWork.ClassRoom.GetAllAsync(c => c.SchoolID == y.SchoolID);
            if (clsList == null)
            {
                TempData["StatusMessage"] = $"Error : Please create class room from manage school";
                return LocalRedirect("/Lms/Homeworks/Index");
            }

            Input.ClassRoomList = clsList.Select(i => new SelectListItem
            {
                Text = i.ClassRoomName,
                Value = i.ClassRoomID.ToString()
            });

            Input.StatusMessage = "Error : Invalid input data.";
            return View(Input);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string UserName, string UserPassword)
        {
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                TempData["StatusMessage"] = $"Error : Invalid Username / User ID";
                return LocalRedirect("/Lms/Schools/Index");
            }

            var tmp = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == user.Id && c.ClassRoom.School.OwnerId == _userId);
            if (tmp != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, UserPassword);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = $"Reset successful";
                    return LocalRedirect("/Lms/Schools/Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["StatusMessage"] = $"Error : Error occurred";
                return LocalRedirect("/Lms/Schools/Index");
            }
            else
            {
                TempData["StatusMessage"] = $"Error : You are not authorized.";
                return LocalRedirect("/Lms/Schools/Index");
            }
        }
    }
}