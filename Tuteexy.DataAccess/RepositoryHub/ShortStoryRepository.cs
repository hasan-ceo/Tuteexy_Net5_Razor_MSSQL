using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class ShortStoryRepository : RepositoryAsync<ShortStory>, IShortStoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ShortStoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShortStory shortstory)
        {
            var objFromDb = _db.ShortStory.FirstOrDefault(s => s.ShortStoryID == shortstory.ShortStoryID);
            if (objFromDb != null)
            {
                objFromDb.Title = shortstory.Title;
                objFromDb.Description = shortstory.Description;
                objFromDb.ImageUrl= shortstory.ImageUrl;
                objFromDb.SubmittedDate = shortstory.SubmittedDate;
                objFromDb.IsApproved = shortstory.IsApproved;
                objFromDb.IsReplyClose = shortstory.IsReplyClose;
                objFromDb.IsOffensive = shortstory.IsOffensive;
            }
        }
    }
}
