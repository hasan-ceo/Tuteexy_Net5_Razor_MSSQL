using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class QuestionRepository : RepositoryAsync<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _db;

        public QuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Question question)
        {
            var objFromDb = _db.Question.FirstOrDefault(s => s.QuestionID == question.QuestionID);
            if (objFromDb != null)
            {
                objFromDb.Description = question.Description;
                objFromDb.SubmittedDate = question.SubmittedDate;
                objFromDb.IsApproved = question.IsApproved;
                objFromDb.IsReplyClose = question.IsReplyClose;
                objFromDb.IsOffensive = question.IsOffensive;
            }
        }
    }
}
