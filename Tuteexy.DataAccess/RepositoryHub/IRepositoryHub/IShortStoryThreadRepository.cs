using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IShortStoryThreadRepository : IRepositoryAsync<ShortStoryThread>
    {
        void Update(ShortStoryThread shortstorythread);
    }
}
