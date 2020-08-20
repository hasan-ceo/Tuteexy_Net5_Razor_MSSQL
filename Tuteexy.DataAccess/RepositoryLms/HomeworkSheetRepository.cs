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

        public void Update(HomeworkSheet homeworkreply)
        {
            var objFromDb = _db.HomeworkReply.FirstOrDefault(s => s.HomeworkID == homeworkreply.HomeworkID);
            if (objFromDb != null)
            {
                objFromDb.Description = homeworkreply.Description;
                objFromDb.DateSubmitted = homeworkreply.DateSubmitted;
                objFromDb.AttachLink1 = homeworkreply.AttachLink1;
                objFromDb.AttachLink2 = homeworkreply.AttachLink2;
                objFromDb.AttachLink3 = homeworkreply.AttachLink3;
                objFromDb.AttachLink4 = homeworkreply.AttachLink4;
                objFromDb.AttachLink5 = homeworkreply.AttachLink5;
                objFromDb.HwMarks = homeworkreply.HwMarks;
                objFromDb.HWStatus = homeworkreply.HWStatus;
                objFromDb.HWComments = homeworkreply.HWComments;
            }
        }

    }
}
