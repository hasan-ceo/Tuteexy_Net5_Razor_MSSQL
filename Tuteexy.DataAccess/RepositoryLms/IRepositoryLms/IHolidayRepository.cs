using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IHolidayRepository : IRepositoryAsync<Holiday>
    {
        void Update(Holiday holiday);
    }
}
