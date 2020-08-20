using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoomStudentRepository : IRepositoryAsync<ClassRoomStudent>
    {
        void Update(ClassRoomStudent classroomstudent);
    }
}
