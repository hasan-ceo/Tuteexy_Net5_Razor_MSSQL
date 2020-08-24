using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassworkAttendanceRepository : IRepositoryAsync<ClassworkAttendance>
    {
        void Update(ClassworkAttendance Classworkattendance);
    }
}
