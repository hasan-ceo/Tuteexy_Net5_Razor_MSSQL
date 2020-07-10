using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
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
            var objFromDb = _db.Pages.FirstOrDefault(s => s.PageID == page.PageID);
            if (objFromDb != null)
            {
                objFromDb.PageName = page.PageName;
                objFromDb.Description = page.Description;

            }
        }
    }
}
