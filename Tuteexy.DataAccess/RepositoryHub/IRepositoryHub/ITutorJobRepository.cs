using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ITutorJobRepository : IRepositoryAsync<TutorJob>
    {
        void Update(TutorJob subject);
    }
}
