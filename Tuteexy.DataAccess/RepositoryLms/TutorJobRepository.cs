using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class TutorJobRepository : RepositoryAsync<TutorJob>, ITutorJobRepository
    {
        private readonly ApplicationDbContext _db;

        public TutorJobRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TutorJob tutorjob)
        {
            var objFromDb = _db.TutorJob.FirstOrDefault(s => s.TutorJobID == tutorjob.TutorJobID);
            if (objFromDb != null)
            {
                objFromDb.JobTitle = tutorjob.JobTitle;
                objFromDb.Course = tutorjob.Course;
                objFromDb.Subject = tutorjob.Subject;
                objFromDb.Salary = tutorjob.Salary;
                objFromDb.NumberofStudents = tutorjob.NumberofStudents;
                objFromDb.Genderpreference = tutorjob.Genderpreference;
                objFromDb.Requirements = tutorjob.Requirements;
                objFromDb.StreetAddress = tutorjob.StreetAddress;
                objFromDb.City = tutorjob.City;
                objFromDb.State = tutorjob.State;
                objFromDb.PostalCode = tutorjob.PostalCode;
                objFromDb.Country = tutorjob.Country;
            }
        }
    }
}
