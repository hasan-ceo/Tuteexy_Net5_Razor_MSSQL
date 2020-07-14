using Titan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.DataAccess.Repository.IRepository
{
    public interface ISchoolTeachersRepository : IRepositoryAsync<SchoolTeacher>
    {
        void Update(SchoolTeacher schoolteachers);
    }
}
