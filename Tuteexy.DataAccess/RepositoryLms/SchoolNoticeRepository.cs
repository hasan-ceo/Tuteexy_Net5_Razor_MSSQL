using System.Linq;
using System.Threading.Tasks;
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
                objFromDb.NoticeTitle = schoolnotice.NoticeTitle;
                objFromDb.Description = schoolnotice.Description;
                objFromDb.ScheduleDateTime = schoolnotice.ScheduleDateTime;

                objFromDb.UpdatedBy = schoolnotice.UpdatedBy;
                objFromDb.UpdatedDate = schoolnotice.UpdatedDate;
            }
        }

    }
}
