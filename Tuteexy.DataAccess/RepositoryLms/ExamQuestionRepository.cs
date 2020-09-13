using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ExamQuestionRepository : RepositoryAsync<ExamQuestion>, IExamQuestionRepository
    {
        private readonly ApplicationDbContext _db;

        public ExamQuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ExamQuestion examquestion)
        {
            var objFromDb = _db.ExamQuestion.FirstOrDefault(s => s.ExamQuestionID == examquestion.ExamQuestionID);
            if (objFromDb != null)
            {
          
                objFromDb.Title = examquestion.Title;
                objFromDb.Option1 = examquestion.Option1;
                objFromDb.Option2 = examquestion.Option2;
                objFromDb.Option3 = examquestion.Option3;
                objFromDb.Option4 = examquestion.Option4;
                objFromDb.CorrectAnswer = examquestion.CorrectAnswer;
                objFromDb.ImageUrl = examquestion.ImageUrl;
                objFromDb.Marks = examquestion.Marks;
                objFromDb.Qtype = examquestion.Qtype;
            }
        }

    }
}
