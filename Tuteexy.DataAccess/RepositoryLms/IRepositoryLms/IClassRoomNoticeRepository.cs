using Tuteexy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IClassRoomNoticeRepository : IRepositoryAsync<ClassRoomNotice>
    {
        void Update(ClassRoomNotice classroomnotice);
    }
}
