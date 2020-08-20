using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoomRepository : IRepositoryAsync<ClassRoom>
    {
        void Update(ClassRoom classroom);
    }
}
