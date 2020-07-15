using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //IM Start
        IPagesRepository Pages { get; }

        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        IuserlistRepository userlist { get; }
        //IM End

        //Lms start
        ISchoolsRepository Schools { get; }
        IClassRoomsRepository ClassRooms { get; }
        IClassRoomStudentsRepository ClassRoomStudents { get; }
        ISchoolTeachersRepository SchoolTeachers { get; }
        IHomeworksRepository Homeworks { get; }
        ISubjectsRepository Subjects { get; }
        //Lms End


        void Save();
    }
}
