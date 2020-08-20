using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IShortStoryRepository : IRepositoryAsync<ShortStory>
    {
        void Update(ShortStory shortstory);
    }
}
