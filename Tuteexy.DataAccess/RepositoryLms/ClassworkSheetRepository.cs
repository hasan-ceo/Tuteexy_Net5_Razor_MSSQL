using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ClassworkSheetRepository : RepositoryAsync<ClassworkSheet>, IClassworkSheetRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassworkSheetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassworkSheet classworksheet)
        {
            var objFromDb = _db.ClassworkSheet.FirstOrDefault(s => s.ClassworkSheetID == classworksheet.ClassworkSheetID);
            if (objFromDb != null)
            {
                objFromDb.Description = classworksheet.Description;
                objFromDb.SubmittedDate = classworksheet.SubmittedDate;
            }
        }
    }
}
