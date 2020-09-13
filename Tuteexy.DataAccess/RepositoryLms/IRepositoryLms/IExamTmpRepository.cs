using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IExamTmpRepository : IRepositoryAsync<ExamTmp>
    {
        void Update(ExamTmp examtmp);
    }
}
