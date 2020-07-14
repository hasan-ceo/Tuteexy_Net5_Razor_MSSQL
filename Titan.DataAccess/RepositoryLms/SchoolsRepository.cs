using System.Linq;
using System.Threading.Tasks;
using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;

namespace Titan.DataAccess.Repository
{
    public class SchoolsRepository : RepositoryAsync<School>, ISchoolsRepository
    {
        private readonly ApplicationDbContext _db;

        public SchoolsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(School school)
        {
            var objFromDb = _db.Schools.FirstOrDefault(s => s.SchoolID == school.SchoolID);
            if (objFromDb != null)
            {
                objFromDb.StreetAddress = school.StreetAddress;
                objFromDb.City = school.City;
                objFromDb.State = school.State;
                objFromDb.PostalCode = school.PostalCode;
                objFromDb.PhoneNumber = school.PhoneNumber;

                objFromDb.UpdatedBy = school.UpdatedBy;
                objFromDb.UpdatedDate = school.UpdatedDate;

            }
        }

    }
}
