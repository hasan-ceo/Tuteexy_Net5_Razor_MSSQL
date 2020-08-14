using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class UserProfileRepository : RepositoryAsync<UserProfile>, IUserProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public UserProfileRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserProfile userprofile)
        {
            var objFromDb = _db.UserProfile.FirstOrDefault(s => s.UserProfileID == userprofile.UserProfileID);
            if (objFromDb != null)
            {
                objFromDb.FatherName = userprofile.FatherName;
                objFromDb.MotherName = userprofile.MotherName;
                objFromDb.DateOfBirth = userprofile.DateOfBirth;
                objFromDb.BloodGroup = userprofile.BloodGroup;
                objFromDb.Gender = userprofile.Gender;
                objFromDb.Religion = userprofile.Religion;
                objFromDb.StreetAddress = userprofile.StreetAddress;
                objFromDb.City = userprofile.City;
                objFromDb.State = userprofile.State;
                objFromDb.PostalCode = userprofile.PostalCode;
                objFromDb.Country = userprofile.Country;
                objFromDb.ECPersonName = userprofile.ECPersonName;
                objFromDb.ECPersonEmail= userprofile.ECPersonEmail;
                objFromDb.ECPersonRelation = userprofile.ECPersonRelation;
                objFromDb.ECPersonPhoneNumber = userprofile.ECPersonPhoneNumber;
                objFromDb.ImageUrl = userprofile.ImageUrl;
            }
        }
    }
}
