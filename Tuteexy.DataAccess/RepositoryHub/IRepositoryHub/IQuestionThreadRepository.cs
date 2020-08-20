using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IQuestionThreadRepository : IRepositoryAsync<QuestionThread>
    {
        void Update(QuestionThread questionthread);
    }
}
