using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;

namespace Tuteexy.ViewComponents
{
    public class StudentMenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentMenuViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var allObj = await _unitOfWork.ClassRoomStudent.GetFirstOrDefaultAsync(c => c.StudentID == claims.Value);
            return View(allObj);
        }
    }
}
