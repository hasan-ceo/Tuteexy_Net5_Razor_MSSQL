using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ICourseRepository : IRepositoryAsync<Course>
    {
        void Update(Course course);
    }
}
