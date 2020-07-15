using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoomStudentsRepository : IRepositoryAsync<ClassRoomStudent>
    {
        void Update(ClassRoomStudent classroomstudent);
    }
}
