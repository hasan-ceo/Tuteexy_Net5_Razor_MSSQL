using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class SchoolRepository : RepositoryAsync<School>, ISchoolRepository
    {
        private readonly ApplicationDbContext _db;

        public SchoolRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(School school)
        {
            _db.Update(school);
        }
    }
}
