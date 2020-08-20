using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IHomeworkSheetRepository : IRepositoryAsync<HomeworkSheet>
    {
        void Update(HomeworkSheet homeworkreply);
    }
}
