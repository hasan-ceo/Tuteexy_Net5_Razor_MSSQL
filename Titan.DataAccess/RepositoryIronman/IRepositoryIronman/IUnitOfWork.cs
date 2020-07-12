using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPageRepository Pages { get; }
        ICoverTypeRepository CoverType { get; }

        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        IuserlistRepository userlist { get; }
        void Save();
    }
}
