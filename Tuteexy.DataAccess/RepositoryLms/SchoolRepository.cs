using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
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
            var objFromDb = _db.School.FirstOrDefault(s => s.SchoolID == school.SchoolID);
            if (objFromDb != null)
            {
                objFromDb.StreetAddress = school.StreetAddress;
                objFromDb.City = school.City;
                objFromDb.State = school.State;
                objFromDb.PostalCode = school.PostalCode;
                objFromDb.PhoneNumber = school.PhoneNumber;
                objFromDb.Country = school.Country;
                objFromDb.UpdatedBy = school.UpdatedBy;
                objFromDb.UpdatedDate = school.UpdatedDate;

            }
        }

    }
}
