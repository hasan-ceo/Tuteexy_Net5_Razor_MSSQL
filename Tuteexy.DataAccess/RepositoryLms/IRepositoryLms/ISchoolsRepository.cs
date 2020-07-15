using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISchoolsRepository : IRepositoryAsync<School>
    {
        void Update(School school);
    }
}
