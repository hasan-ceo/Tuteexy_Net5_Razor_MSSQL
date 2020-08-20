using System.Linq;
using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository
{
    public class userlistRepository : RepositoryAsync<userlist>, IuserlistRepository
    {
        private readonly ApplicationDbContext _db;

        public userlistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(userlist userlist)
        {
            var objFromDb = _db.userlist.FirstOrDefault(s => s.Id == userlist.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = userlist.Name;

            }
        }
    }
}
