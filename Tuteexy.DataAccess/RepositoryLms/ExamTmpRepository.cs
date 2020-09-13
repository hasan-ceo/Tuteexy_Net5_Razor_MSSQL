using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ExamTmpRepository : RepositoryAsync<ExamTmp>, IExamTmpRepository
    {
        private readonly ApplicationDbContext _db;

        public ExamTmpRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ExamTmp examtmp)
        {
            var objFromDb = _db.ExamTmp.FirstOrDefault(s => s.ExamTmpID == examtmp.ExamTmpID);
            if (objFromDb != null)
            {
                objFromDb.Subject = examtmp.Subject;
                objFromDb.Title = examtmp.Title;
                objFromDb.ExmMarks = examtmp.ExmMarks;
                objFromDb.Description = examtmp.Description;
                objFromDb.DateDue = examtmp.DateDue;
                objFromDb.ScheduleDateTime = examtmp.ScheduleDateTime;
                objFromDb.RefLink1 = examtmp.RefLink1;
                objFromDb.RefLink2 = examtmp.RefLink2;
                objFromDb.RefLink3 = examtmp.RefLink3;
                objFromDb.RefLink4 = examtmp.RefLink4;
                objFromDb.RefLink5 = examtmp.RefLink5;
            }
        }

    }
}
