using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassworkSheetRepository : IRepositoryAsync<ClassworkSheet>
    {
        void Update(ClassworkSheet classworksheet);
    }
}
