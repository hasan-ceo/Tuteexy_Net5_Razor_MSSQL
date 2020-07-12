using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IPageRepository Pages { get; }

        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        IuserlistRepository userlist { get; }

        //Lms start
        ISchoolRepository Schools { get; }
        //Lms End


        void Save();
    }
}
