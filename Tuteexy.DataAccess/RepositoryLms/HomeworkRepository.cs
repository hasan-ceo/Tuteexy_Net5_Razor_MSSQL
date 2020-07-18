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
            var objFromDb = _db.Homeworks.FirstOrDefault(s => s.HomeworkID == homework.HomeworkID);
            if (objFromDb != null)
            {
                objFromDb.Subject = homework.Subject;
                objFromDb.Title = homework.Title;
                objFromDb.Description = homework.Description;
                objFromDb.DateDue = homework.DateDue;
                objFromDb.RefLink = homework.RefLink;
            }
        }

    }
}
