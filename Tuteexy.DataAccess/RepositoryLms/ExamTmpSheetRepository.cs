using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ExamTmpSheetRepository : RepositoryAsync<ExamTmpSheet>, IExamTmpSheetRepository
    {
        private readonly ApplicationDbContext _db;

        public ExamTmpSheetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ExamTmpSheet examtmpsheet)
        {
            var objFromDb = _db.ExamTmpSheet.FirstOrDefault(s => s.ExamTmpID == examtmpsheet.ExamTmpID);
            if (objFromDb != null)
            {
                objFromDb.Description = examtmpsheet.Description;
                objFromDb.DateSubmitted = examtmpsheet.DateSubmitted;
                objFromDb.AttachLink1 = examtmpsheet.AttachLink1;
                objFromDb.AttachLink2 = examtmpsheet.AttachLink2;
                objFromDb.AttachLink3 = examtmpsheet.AttachLink3;
                objFromDb.AttachLink4 = examtmpsheet.AttachLink4;
                objFromDb.AttachLink5 = examtmpsheet.AttachLink5;
                objFromDb.AttachLink6 = examtmpsheet.AttachLink6;
                objFromDb.AttachLink7 = examtmpsheet.AttachLink7;
                objFromDb.AttachLink8 = examtmpsheet.AttachLink8;
                objFromDb.AttachLink9 = examtmpsheet.AttachLink9;
                objFromDb.AttachLink10 = examtmpsheet.AttachLink10;
                objFromDb.AttachLink11 = examtmpsheet.AttachLink11;
                objFromDb.AttachLink12 = examtmpsheet.AttachLink12;
                objFromDb.AttachLink13 = examtmpsheet.AttachLink13;
                objFromDb.AttachLink14 = examtmpsheet.AttachLink14;
                objFromDb.AttachLink15 = examtmpsheet.AttachLink15;
                objFromDb.ExmMarks = examtmpsheet.ExmMarks;
                objFromDb.ExmStatus = examtmpsheet.ExmStatus;
                objFromDb.ExmComments = examtmpsheet.ExmComments;
            }
        }

    }
}
