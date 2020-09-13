using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class HomeworkSheetRepository : RepositoryAsync<HomeworkSheet>, IHomeworkSheetRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeworkSheetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(HomeworkSheet homeworksheet)
        {
            var objFromDb = _db.HomeworkSheet.FirstOrDefault(s => s.HomeworkID == homeworksheet.HomeworkID);
            if (objFromDb != null)
            {
                objFromDb.Description = homeworksheet.Description;
                objFromDb.DateSubmitted = homeworksheet.DateSubmitted;
                objFromDb.AttachLink1 = homeworksheet.AttachLink1;
                objFromDb.AttachLink2 = homeworksheet.AttachLink2;
                objFromDb.AttachLink3 = homeworksheet.AttachLink3;
                objFromDb.AttachLink4 = homeworksheet.AttachLink4;
                objFromDb.AttachLink5 = homeworksheet.AttachLink5;
                objFromDb.HwMarks = homeworksheet.HwMarks;
                objFromDb.HWStatus = homeworksheet.HWStatus;
                objFromDb.HWComments = homeworksheet.HWComments;
            }
        }

    }
}
