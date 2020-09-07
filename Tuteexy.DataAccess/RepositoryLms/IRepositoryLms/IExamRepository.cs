using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IExamRepository : IRepositoryAsync<Exam>
    {
        void Update(Exam exam);
    }
}
