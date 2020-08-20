using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IQuestionRepository : IRepositoryAsync<Question>
    {
        void Update(Question question);
    }
}
