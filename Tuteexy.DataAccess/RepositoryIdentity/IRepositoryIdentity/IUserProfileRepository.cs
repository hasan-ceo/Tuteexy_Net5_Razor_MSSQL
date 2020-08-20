using Tuteexy.Models;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IUserProfileRepository : IRepositoryAsync<UserProfile>
    {
        void Update(UserProfile userprofile);
    }
}
