using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassRoomStudentRepository : RepositoryAsync<ClassRoomStudent>, IClassRoomStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassRoomStudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassRoomStudent classroomstudent)
        {
            var objFromDb = _db.ClassRoomStudent.FirstOrDefault(s => s.ClassRoomStudentID == classroomstudent.ClassRoomStudentID);
            if (objFromDb != null)
            {

                objFromDb.ApprovedBy = classroomstudent.ApprovedBy;
                objFromDb.ApprovedDate = classroomstudent.ApprovedDate;
                objFromDb.IsApproved = classroomstudent.IsApproved;


            }
        }
    }
}
