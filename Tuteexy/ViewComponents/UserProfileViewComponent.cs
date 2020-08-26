using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == claims.Value);
            var userFromDb = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(u => u.UserID == claims.Value, includeProperties: "User");
            if (userFromDb == null)
            {
                UserProfile userprofile = new UserProfile
                {
                    UserID = claims.Value,
                    FatherName = "",
                    MotherName = "",
                    DateOfBirth = DateTime.Now.Date,
                    Gender = "N/A",
                    Religion = "N/A",
                    BloodGroup = "N/A",
                    StreetAddress = "",
                    City = "",
                    State = "",
                    PostalCode = "",
                    Country = "Bangladesh",
                    ECPersonName = "",
                    ECPersonRelation = "",
                    ECPersonPhoneNumber = "",
                    ECPersonEmail = "",
                    ImageUrl = "profile.jpg",
                    FullName=user.FullName
                };
                await _unitOfWork.UserProfile.AddAsync(userprofile);
                _unitOfWork.Save();

                var newprofile = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(u => u.UserID == userprofile.UserID, includeProperties: "User");

                return View(newprofile);
            }
            return View(userFromDb);
        }
    }
}
