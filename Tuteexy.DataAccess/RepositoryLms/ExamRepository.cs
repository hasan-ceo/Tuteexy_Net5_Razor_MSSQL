using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ExamRepository : RepositoryAsync<Exam>, IExamRepository
    {
        private readonly ApplicationDbContext _db;

        public ExamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Exam homework)
        {
            var objFromDb = _db.Exam.FirstOrDefault(s => s.ExamID == homework.ExamID);
            if (objFromDb != null)
            {
                objFromDb.Subject = homework.Subject;
                objFromDb.Title = homework.Title;
                objFromDb.TimeStart = homework.TimeStart;
                objFromDb.TimeEnd = homework.TimeEnd;
            }
        }

    }
}
