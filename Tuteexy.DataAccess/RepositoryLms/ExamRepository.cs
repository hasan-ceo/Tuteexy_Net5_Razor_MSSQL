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

        public void Update(Exam exam)
        {
            var objFromDb = _db.Exam.FirstOrDefault(s => s.ExamID == exam.ExamID);
            if (objFromDb != null)
            {
                objFromDb.Subject = exam.Subject;
                objFromDb.Title = exam.Title;
                objFromDb.TimeStart = exam.TimeStart;
                objFromDb.TimeEnd = exam.TimeEnd;
            }
        }

    }
}
