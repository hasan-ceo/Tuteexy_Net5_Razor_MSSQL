using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IExamTmpSheetRepository : IRepositoryAsync<ExamTmpSheet>
    {
        void Update(ExamTmpSheet examtmpsheet);
    }
}
