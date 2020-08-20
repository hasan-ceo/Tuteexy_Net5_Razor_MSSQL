using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class CourseThreadRepository : RepositoryAsync<CourseThread>, ICourseThreadRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CourseThread coursethread)
        {
            var objFromDb = _db.CourseThread.FirstOrDefault(s => s.CourseThreadID == coursethread.CourseThreadID);
            if (objFromDb != null)
            {
                objFromDb.Description = coursethread.Description;
                objFromDb.SubmittedDate = coursethread.SubmittedDate;
                objFromDb.IsApproved = coursethread.IsApproved;
                objFromDb.IsReplyClose = coursethread.IsReplyClose;
                objFromDb.IsOffensive = coursethread.IsOffensive;
            }
        }
    }
}
