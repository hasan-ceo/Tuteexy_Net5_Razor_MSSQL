using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using Titan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class PagesRepository : RepositoryAsync<Page>, IPagesRepository
    {
        private readonly ApplicationDbContext _db;

        public PagesRepository(ApplicationDbContext db) : base(db)
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

                objFromDb.UpdatedBy = page.UpdatedBy;
                objFromDb.UpdatedDate = page.UpdatedDate;

            }
        }
    }
}
