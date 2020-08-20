using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassRoomNoticeRepository : RepositoryAsync<ClassRoomNotice>, IClassRoomNoticeRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassRoomNoticeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassRoomNotice classroomnotice)
        {
            var objFromDb = _db.ClassRoomNotice.FirstOrDefault(s => s.ClassRoomNoticeID == classroomnotice.ClassRoomNoticeID);
            if (objFromDb != null)
            {
                objFromDb.Title = classroomnotice.Title;
                objFromDb.Description = classroomnotice.Description;
                objFromDb.ScheduleDateTime = classroomnotice.ScheduleDateTime;

                objFromDb.UpdatedBy = classroomnotice.UpdatedBy;
                objFromDb.UpdatedDate = classroomnotice.UpdatedDate;
            }
        }

    }
}
