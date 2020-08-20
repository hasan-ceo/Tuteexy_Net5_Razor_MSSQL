using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassworkRepository : IRepositoryAsync<Classwork>
    {
        void Update(Classwork homework);
    }
}
