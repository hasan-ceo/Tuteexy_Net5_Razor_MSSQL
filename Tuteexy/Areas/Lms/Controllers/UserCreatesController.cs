using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
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

            UserCreateVM usercreatevm = new UserCreateVM {
                TmpID = Id
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
                    Name = Input.Name,
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
                        SchoolID = Input.TmpID,
                        TeacherID= user.Id,
                        ApprovedBy = User.Identity.Name,
                        ApprovedDate =DateTime.Now,
                        IsApproved=true
                    };
                    await _unitOfWork.SchoolTeacher.AddAsync(st);
                    _unitOfWork.Save();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
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
                        user.Name,
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

    }
}