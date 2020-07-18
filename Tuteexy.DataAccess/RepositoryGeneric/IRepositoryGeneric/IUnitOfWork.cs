using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //IM Start
        IPageRepository Page { get; }

        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        IuserlistRepository userlist { get; }
        //IM End

        //Lms start
        ISchoolRepository School { get; }
        IClassRoomRepository ClassRoom { get; }
        IClassRoomStudentRepository ClassRoomStudent { get; }
        ISchoolTeacherRepository SchoolTeacher { get; }
        IHomeworkRepository Homework { get; }
        ISubjectRepository Subject { get; }
        //Lms End


        void Save();
    }
}
