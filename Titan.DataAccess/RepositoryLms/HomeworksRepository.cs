using System.Linq;
using System.Threading.Tasks;
using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;

namespace Titan.DataAccess.Repository
{
    public class HomeworksRepository : RepositoryAsync<Homework>, IHomeworksRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeworksRepository(ApplicationDbContext db) : base(db)
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
            }
        }

    }
}
