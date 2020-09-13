using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IExamQuestionRepository : IRepositoryAsync<ExamQuestion>
    {
        void Update(ExamQuestion examquestion);
    }
}
