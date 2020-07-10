using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class userlistRepository : Repository<userlist>, IuserlistRepository
    {
        private readonly ApplicationDbContext _db;

        public userlistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(userlist userlist)
        {
            var objFromDb = _db.userlist.FirstOrDefault(s => s.Id == userlist.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = userlist.Name;
               
            }
        }
    }
}
