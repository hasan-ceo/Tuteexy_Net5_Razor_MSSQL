using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoomNoticeRepository : IRepositoryAsync<ClassRoomNotice>
    {
        void Update(ClassRoomNotice classroomnotice);
    }
}
