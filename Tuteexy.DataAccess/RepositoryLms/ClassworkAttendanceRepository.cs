using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassworkAttendanceRepository : RepositoryAsync<ClassworkAttendance>, IClassworkAttendanceRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassworkAttendanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassworkAttendance classworksheet)
        {
            var objFromDb = _db.ClassworkAttendance.FirstOrDefault(s => s.ClassworkAttendanceID == classworksheet.ClassworkAttendanceID);
            if (objFromDb != null)
            {
                objFromDb.WorkDate = classworksheet.WorkDate;
            }
        }
    }
}
