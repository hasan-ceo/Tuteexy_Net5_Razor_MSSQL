using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class HomeworkRepository : RepositoryAsync<Homework>, IHomeworkRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeworkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Homework homework)
        {
            var objFromDb = _db.Homework.FirstOrDefault(s => s.HomeworkID == homework.HomeworkID);
            if (objFromDb != null)
            {
                objFromDb.Subject = homework.Subject;
                objFromDb.Title = homework.Title;
                objFromDb.HwMarks = homework.HwMarks;
                objFromDb.Description = homework.Description;
                objFromDb.DateDue = homework.DateDue;
                objFromDb.ScheduleDateTime = homework.ScheduleDateTime;
                objFromDb.RefLink1 = homework.RefLink1;
                objFromDb.RefLink2 = homework.RefLink2;
                objFromDb.RefLink3 = homework.RefLink3;
                objFromDb.RefLink4 = homework.RefLink4;
                objFromDb.RefLink5 = homework.RefLink5;
            }
        }

    }
}
