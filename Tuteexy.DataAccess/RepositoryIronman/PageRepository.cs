using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class PageRepository : RepositoryAsync<Page>, IPageRepository
    {
        private readonly ApplicationDbContext _db;

        public PageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Page page)
        {
            var objFromDb = _db.Page.FirstOrDefault(s => s.PageID == page.PageID);
            if (objFromDb != null)
            {
                objFromDb.PageName = page.PageName;
                objFromDb.Description = page.Description;

                objFromDb.UpdatedBy = page.UpdatedBy;
                objFromDb.UpdatedDate = page.UpdatedDate;

            }
        }
    }
}
