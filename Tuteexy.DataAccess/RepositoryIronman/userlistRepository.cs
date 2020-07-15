﻿using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class userlistRepository : RepositoryAsync<userlist>, IuserlistRepository
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