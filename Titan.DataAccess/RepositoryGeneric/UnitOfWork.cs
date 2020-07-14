using Titan.DataAccess.Data;
using Titan.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Pages = new PagesRepository(_db);
            ClassRooms = new ClassRoomsRepository(_db);
            ClassRoomStudents = new ClassRoomStudentsRepository(_db);
            userlist = new userlistRepository(_db);
            Schools = new SchoolsRepository(_db);
            SchoolTeachers = new SchoolTeachersRepository(_db);
            Homeworks = new HomeworksRepository(_db);
            Subjects = new SubjectsRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);

        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPagesRepository Pages { get; private set; }

        public ISP_Call SP_Call { get; private set; }
        public IuserlistRepository userlist { get; private set; }


        public ISchoolsRepository Schools { get; private set; }
        public ISchoolTeachersRepository SchoolTeachers { get; private set; }
        public IClassRoomsRepository ClassRooms { get; private set; }
        public IClassRoomStudentsRepository ClassRoomStudents { get; private set; }
        public IHomeworksRepository Homeworks { get; private set; }
        public ISubjectsRepository Subjects { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
