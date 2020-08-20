using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISchoolTeacherRepository : IRepositoryAsync<SchoolTeacher>
    {
        void Update(SchoolTeacher schoolteachers);
    }
}
