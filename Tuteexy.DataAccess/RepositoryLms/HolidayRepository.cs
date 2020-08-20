using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class HolidayRepository : RepositoryAsync<Holiday>, IHolidayRepository
    {
        private readonly ApplicationDbContext _db;

        public HolidayRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Holiday holiday)
        {
            var objFromDb = _db.Holiday.FirstOrDefault(s => s.HolidayID == holiday.HolidayID);
            if (objFromDb != null)
            {
                objFromDb.DateStart = holiday.DateStart;
                objFromDb.DateEnd = holiday.DateEnd;
                objFromDb.HolidayName = holiday.HolidayName;
                objFromDb.Duration = holiday.Duration;

                objFromDb.UpdatedBy = holiday.UpdatedBy;
                objFromDb.UpdatedDate = holiday.UpdatedDate;
            }
        }

    }
}
