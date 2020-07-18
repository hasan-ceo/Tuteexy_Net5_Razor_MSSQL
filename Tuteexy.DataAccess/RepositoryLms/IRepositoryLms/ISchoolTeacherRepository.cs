using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISchoolTeacherRepository : IRepositoryAsync<SchoolTeacher>
    {
        void Update(SchoolTeacher schoolteachers);
    }
}
