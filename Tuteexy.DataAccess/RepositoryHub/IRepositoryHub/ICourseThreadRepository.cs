using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ICourseThreadRepository : IRepositoryAsync<CourseThread>
    {
        void Update(CourseThread coursethread);
    }
}
