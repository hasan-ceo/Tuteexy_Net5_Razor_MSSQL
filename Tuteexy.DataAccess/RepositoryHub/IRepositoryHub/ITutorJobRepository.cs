using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ITutorJobRepository : IRepositoryAsync<TutorJob>
    {
        void Update(TutorJob subject);
    }
}
