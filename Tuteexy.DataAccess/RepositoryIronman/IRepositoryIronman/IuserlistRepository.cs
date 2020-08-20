using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IuserlistRepository : IRepositoryAsync<userlist>
    {
        void Update(userlist userlist);
    }
}
