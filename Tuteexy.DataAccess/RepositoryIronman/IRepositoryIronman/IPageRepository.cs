using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IPageRepository : IRepositoryAsync<Page>
    {
        void Update(Page page);
    }
}
