using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Pages = new PageRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            userlist = new userlistRepository(_db);
            Company = new CompanyRepository(_db);

            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);

        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPageRepository Pages { get; private set; }
        public ICompanyRepository Company { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public ISP_Call SP_Call { get; private set; }
        public IuserlistRepository userlist { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
