using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models.ViewModels;

namespace Tuteexy.ViewComponents
{
    public class QuestionAViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionAViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(long Id)
        {
            var question = await _unitOfWork.Question.GetFirstOrDefaultAsync(q => q.QuestionID == Id, includeProperties: "User");
            var questionthread = await _unitOfWork.QuestionThread.GetAllAsync(q => q.QuestionID == Id, includeProperties: "User");
            QuestionVM questionVM = new QuestionVM
            {
                Question = question,
                QuestionThread = questionthread
            };
            //var allObj = await _unitOfWork.Question.GetAllAsync(c => c.CreatedBy == User.Identity.Name);
            return View(questionVM);
        }
    }
}
