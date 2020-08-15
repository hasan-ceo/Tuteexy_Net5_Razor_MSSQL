using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ICourseThreadRepository : IRepositoryAsync<CourseThread>
    {
        void Update(CourseThread coursethread);
    }
}
