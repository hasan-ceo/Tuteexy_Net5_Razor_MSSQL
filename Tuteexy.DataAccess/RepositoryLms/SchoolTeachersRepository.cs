﻿using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class SchoolTeachersRepository : RepositoryAsync<SchoolTeacher>, ISchoolTeachersRepository
    {
        private readonly ApplicationDbContext _db;

        public SchoolTeachersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SchoolTeacher schoolteacher)
        {
            var objFromDb = _db.SchoolTeachers.FirstOrDefault(s => s.SchoolTeacherID == schoolteacher.SchoolTeacherID);
            if (objFromDb != null)
            {
                objFromDb.ApprovedBy = schoolteacher.ApprovedBy;
                objFromDb.ApprovedDate = schoolteacher.ApprovedDate;
                objFromDb.IsApproved = schoolteacher.IsApproved;  
            }
        }
    }
}