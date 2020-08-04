using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassRoutineRepository : RepositoryAsync<ClassRoutine>, IClassRoutineRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassRoutineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassRoutine classroutine)
        {
            var objFromDb = _db.ClassRoutine.FirstOrDefault(s => s.ClassRoutineID == classroutine.ClassRoutineID);
            if (objFromDb != null)
            {
                objFromDb.Period1 = classroutine.Period1;
                objFromDb.Period2 = classroutine.Period2;
                objFromDb.Period3 = classroutine.Period3;
                objFromDb.Period4 = classroutine.Period4;
                objFromDb.Period5 = classroutine.Period5;
                objFromDb.Period6 = classroutine.Period6;
                objFromDb.Period7 = classroutine.Period7;
                objFromDb.Period8 = classroutine.Period8;
                objFromDb.Period9 = classroutine.Period9;
                objFromDb.Period10 = classroutine.Period10;

                objFromDb.PeriodTime1 = classroutine.PeriodTime1;
                objFromDb.PeriodTime2 = classroutine.PeriodTime2;
                objFromDb.PeriodTime3 = classroutine.PeriodTime3;
                objFromDb.PeriodTime4 = classroutine.PeriodTime4;
                objFromDb.PeriodTime5 = classroutine.PeriodTime5;
                objFromDb.PeriodTime6 = classroutine.PeriodTime6;
                objFromDb.PeriodTime7 = classroutine.PeriodTime7;
                objFromDb.PeriodTime8 = classroutine.PeriodTime8;
                objFromDb.PeriodTime9 = classroutine.PeriodTime9;
                objFromDb.PeriodTime10 = classroutine.PeriodTime10;

                objFromDb.UpdatedBy = classroutine.UpdatedBy;
                objFromDb.UpdatedDate = classroutine.UpdatedDate;
            }
        }

    }
}
