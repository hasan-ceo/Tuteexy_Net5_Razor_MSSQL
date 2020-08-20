using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISubjectRepository : IRepositoryAsync<Subject>
    {
        void Update(Subject subject);
    }
}
