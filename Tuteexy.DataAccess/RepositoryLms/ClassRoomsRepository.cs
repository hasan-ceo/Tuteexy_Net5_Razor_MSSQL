﻿using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassRoomsRepository : RepositoryAsync<ClassRoom>, IClassRoomsRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassRoomsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassRoom classroom)
        {
            var objFromDb = _db.ClassRooms.FirstOrDefault(s => s.ClassRoomID == classroom.ClassRoomID);
            if (objFromDb != null)
            {
                objFromDb.ClassRoomName = classroom.ClassRoomName;
               
            }
        }
    }
}