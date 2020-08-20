using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IHomeworkRepository : IRepositoryAsync<Homework>
    {
        void Update(Homework homework);
    }
}
