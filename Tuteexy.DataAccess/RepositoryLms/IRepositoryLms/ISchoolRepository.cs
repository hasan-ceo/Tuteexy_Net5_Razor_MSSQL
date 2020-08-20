using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISchoolRepository : IRepositoryAsync<School>
    {
        void Update(School school);
    }
}
