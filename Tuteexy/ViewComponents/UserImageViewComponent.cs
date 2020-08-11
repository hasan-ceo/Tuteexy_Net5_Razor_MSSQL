using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Tuteexy.Models;

namespace Tuteexy.ViewComponents
{
    public class UserImageViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserImageViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Id)
        {
            var userFromDb = await _unitOfWork.UserProfile.GetFirstOrDefaultAsync(u => u.UserID == Id);
            return View(userFromDb.ImageUrl);
        }
    }
}
