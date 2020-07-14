using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class ClassRoomStudentsRepository : RepositoryAsync<ClassRoomStudent>, IClassRoomStudentsRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassRoomStudentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassRoomStudent classroomstudent)
        {
            var objFromDb = _db.ClassRoomStudents.FirstOrDefault(s => s.ClassRoomStudentID == classroomstudent.ClassRoomStudentID);
            if (objFromDb != null)
            {

                objFromDb.ApprovedBy = classroomstudent.ApprovedBy;
                objFromDb.ApprovedDate = classroomstudent.ApprovedDate;
                objFromDb.IsApproved = classroomstudent.IsApproved;
    
               
            }
        }
    }
}
