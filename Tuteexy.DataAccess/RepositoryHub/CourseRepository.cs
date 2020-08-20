using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class CourseRepository : RepositoryAsync<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Course course)
        {
            var objFromDb = _db.Course.FirstOrDefault(s => s.CourseID == course.CourseID);
            if (objFromDb != null)
            {
                objFromDb.Title = course.Title;
                objFromDb.Description = course.Description;
                objFromDb.ImageUrl = course.ImageUrl;
                objFromDb.SubmittedDate = course.SubmittedDate;
                objFromDb.IsApproved = course.IsApproved;
                objFromDb.IsReplyClose = course.IsReplyClose;
                objFromDb.IsOffensive = course.IsOffensive;
            }
        }
    }
}
