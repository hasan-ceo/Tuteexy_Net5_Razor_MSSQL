using Titan.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Titan.DataAccess.Repository.IRepository
{
    public interface IPagesRepository : IRepositoryAsync<Page>
    {
        void Update(Page page);
    }
}
