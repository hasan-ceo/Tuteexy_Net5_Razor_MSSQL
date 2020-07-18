using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
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

namespace Tuteexy.Areas.Front.Controllers
{
    [Area("Front")]
    [AllowAnonymous]
    public class IdentityController : Controller
    {
        

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<IdentityController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public IdentityController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<IdentityController> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("/Front/Identity/Register", Name = "Register")]
        public ActionResult Register(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM,string returnUrl= null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
           
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerVM.Email,
                    Email = registerVM.Email,
                    Name = registerVM.Name,
                    PhoneNumber = registerVM.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, SD.Role_User);
                    _logger.LogInformation("Set Role to User");

                    await _userManager.AddClaimAsync(user, new Claim("InstituteID", Guid.NewGuid().ToString()));
                    _logger.LogInformation("Create a InstituteName");

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


                    await _emailSender.SendEmailAsync(registerVM.Email, "Confirm your email", messageBody);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect("/User/Dashboard/index");


                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return LocalRedirect(returnUrl);
        }

        [HttpGet]     
        [Route("/Front/Identity/Login", Name = "Login")]
        public ActionResult Login(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            return LocalRedirect(returnUrl);
        }

        //login
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginVM loginVM,string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.UserName == loginVM.UserName);
                    string role = (await _signInManager.UserManager.GetRolesAsync(user)).FirstOrDefault();

                    _logger.LogInformation("User logged in.");
                    switch (role)
                    {
                        case SD.Role_Ironman:
                            return LocalRedirect("/Ironman/Dashboard/Index");
                        case SD.Role_User:
                            return LocalRedirect("/User/Dashboard/Index");
                        default:
                            break;
                    }
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return LocalRedirect(returnUrl);
                }
            }
            ModelState.AddModelError("Login", "Invalid login attempt.");
            // If we got this far, something failed, redisplay form
            return LocalRedirect(returnUrl);
        }
    }
}
