using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class QuestionThreadRepository : RepositoryAsync<QuestionThread>, IQuestionThreadRepository
    {
        private readonly ApplicationDbContext _db;

        public QuestionThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(QuestionThread questionthread)
        {
            var objFromDb = _db.QuestionThread.FirstOrDefault(s => s.QuestionThreadID == questionthread.QuestionThreadID);
            if (objFromDb != null)
            {
                objFromDb.Description = questionthread.Description;
                objFromDb.SubmittedDate = questionthread.SubmittedDate;
                objFromDb.IsApproved = questionthread.IsApproved;
                objFromDb.IsReplyClose = questionthread.IsReplyClose;
                objFromDb.IsOffensive = questionthread.IsOffensive;
            }
        }
    }
}
