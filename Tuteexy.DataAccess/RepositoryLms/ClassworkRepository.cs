using System.Linq;
using System.Threading.Tasks;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassworkRepository : RepositoryAsync<Classwork>, IClassworkRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassworkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Classwork classwork)
        {
            var objFromDb = _db.Classwork.FirstOrDefault(s => s.ClassworkID == classwork.ClassworkID);
            if (objFromDb != null)
            {
                objFromDb.Subject = classwork.Subject;
                objFromDb.Title = classwork.Title;
                objFromDb.Description = classwork.Description;
                objFromDb.TimeStart = classwork.TimeStart;
                objFromDb.TimeEnd = classwork.TimeEnd;
                objFromDb.RefLink1 = classwork.RefLink1;
                objFromDb.RefLink2 = classwork.RefLink2;
                objFromDb.RefLink3 = classwork.RefLink3;
                objFromDb.RefLink4 = classwork.RefLink4;
                objFromDb.RefLink5 = classwork.RefLink5;
            }
        }

    }
}
