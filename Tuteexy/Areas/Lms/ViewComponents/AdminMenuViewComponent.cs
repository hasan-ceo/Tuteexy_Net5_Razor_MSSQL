using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Tuteexy.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminMenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var allObj = await _unitOfWork.School.GetFirstOrDefaultAsync(c => c.OwnerId == claims.Value);
            return View(allObj);
        }
    }
}
