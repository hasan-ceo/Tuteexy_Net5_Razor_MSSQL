using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IuserlistRepository : IRepositoryAsync<userlist>
    {
        void Update(userlist userlist);
    }
}
