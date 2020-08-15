using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ICourseRepository : IRepositoryAsync<Course>
    {
        void Update(Course course);
    }
}
