using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoutineRepository : IRepositoryAsync<ClassRoutine>
    {
        void Update(ClassRoutine classroutine);
    }
}
