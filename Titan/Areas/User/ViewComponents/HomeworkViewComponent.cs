using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Titan.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Titan.ViewComponents
{
    public class HomeworkViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeworkViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var allObj = await _unitOfWork.Homeworks.GetAllAsync(h => h.TeacherID == _userId, includeProperties: "ClassRoom,Teacher");
            //return Json(new { data = allObj.Select(a => new { a.ClassRoom.ClassRoomName, a.Subject, a.Title, a.DateDue }) });

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userFromDb = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == claims.Value);

            return View(claims.Value);
        }
    }
}
