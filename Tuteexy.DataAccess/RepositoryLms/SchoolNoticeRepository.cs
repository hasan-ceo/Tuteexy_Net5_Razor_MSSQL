using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class SchoolNoticeRepository : RepositoryAsync<SchoolNotice>, ISchoolNoticeRepository
    {
        private readonly ApplicationDbContext _db;

        public SchoolNoticeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SchoolNotice schoolnotice)
        {
            var objFromDb = _db.SchoolNotice.FirstOrDefault(s => s.SchoolNoticeID == schoolnotice.SchoolNoticeID);
            if (objFromDb != null)
            {
                objFromDb.Title = schoolnotice.Title;
                objFromDb.Description = schoolnotice.Description;
                objFromDb.ScheduleDateTime = schoolnotice.ScheduleDateTime;
                objFromDb.isPined = schoolnotice.isPined;

                objFromDb.UpdatedBy = schoolnotice.UpdatedBy;
                objFromDb.UpdatedDate = schoolnotice.UpdatedDate;
            }
        }

    }
}
