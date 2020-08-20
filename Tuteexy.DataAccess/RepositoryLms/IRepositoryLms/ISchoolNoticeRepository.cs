using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface ISchoolNoticeRepository : IRepositoryAsync<SchoolNotice>
    {
        void Update(SchoolNotice schoolnotice);
    }
}
