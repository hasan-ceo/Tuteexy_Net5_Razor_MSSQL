using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;

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
