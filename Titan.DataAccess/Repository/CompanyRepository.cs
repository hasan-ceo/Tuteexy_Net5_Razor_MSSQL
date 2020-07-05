using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Update(company);
        }
    }
}
