using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Tuteexy.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserNameViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == claims.Value);

            return View(userFromDb);
        }
    }
}
