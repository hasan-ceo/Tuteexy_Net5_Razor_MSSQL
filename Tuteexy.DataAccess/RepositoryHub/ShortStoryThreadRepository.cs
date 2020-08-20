using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class ShortStoryThreadRepository : RepositoryAsync<ShortStoryThread>, IShortStoryThreadRepository
    {
        private readonly ApplicationDbContext _db;

        public ShortStoryThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShortStoryThread shortstorythread)
        {
            var objFromDb = _db.ShortStoryThread.FirstOrDefault(s => s.ShortStoryThreadID == shortstorythread.ShortStoryThreadID);
            if (objFromDb != null)
            {
                objFromDb.Description = shortstorythread.Description;
                objFromDb.SubmittedDate = shortstorythread.SubmittedDate;
                objFromDb.IsApproved = shortstorythread.IsApproved;
                objFromDb.IsReplyClose = shortstorythread.IsReplyClose;
                objFromDb.IsOffensive = shortstorythread.IsOffensive;
            }
        }
    }
}
