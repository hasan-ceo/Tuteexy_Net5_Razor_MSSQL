using Tuteexy.DataAccess.Data;
using Tuteexy.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tuteexy.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);
            Page = new PageRepository(_db);

            ClassRoom = new ClassRoomRepository(_db);
            ClassRoomStudent = new ClassRoomStudentRepository(_db);
            userlist = new userlistRepository(_db);
            School = new SchoolRepository(_db);
            SchoolTeacher = new SchoolTeacherRepository(_db);
            Homework = new HomeworkRepository(_db);
            HomeworkSheet = new HomeworkSheetRepository(_db);
            Classwork = new ClassworkRepository(_db);
            Subject = new SubjectRepository(_db);
            SchoolNotice = new SchoolNoticeRepository(_db);
            ClassRoomNotice = new ClassRoomNoticeRepository(_db);
            ClassRoutine = new ClassRoutineRepository(_db);
            Holiday = new HolidayRepository(_db);
            TutorJob = new TutorJobRepository(_db);
            UserProfile = new UserProfileRepository(_db);
            Question = new QuestionRepository(_db);
            QuestionThread = new QuestionThreadRepository(_db);
            ClassworkSheet = new ClassworkSheetRepository(_db);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPageRepository Page { get; private set; }

        public ISP_Call SP_Call { get; private set; }
        public IuserlistRepository userlist { get; private set; }


        public ISchoolRepository School { get; private set; }
        public ISchoolTeacherRepository SchoolTeacher { get; private set; }
        public IClassRoomRepository ClassRoom { get; private set; }
        public IClassRoomStudentRepository ClassRoomStudent { get; private set; }
        public IHomeworkRepository Homework { get; private set; }
        public IHomeworkSheetRepository HomeworkSheet { get; private set; }
        public IClassworkRepository Classwork { get; private set; }
        public IClassworkSheetRepository ClassworkSheet { get; private set; }
        public ISubjectRepository Subject { get; private set; }
        public ISchoolNoticeRepository SchoolNotice { get; private set; }
        public IClassRoomNoticeRepository ClassRoomNotice { get; private set; }
        public IClassRoutineRepository ClassRoutine { get; private set; }
        public IHolidayRepository Holiday { get; private set; }
        public ITutorJobRepository TutorJob { get; private set; }
        public IUserProfileRepository UserProfile { get; private set; }
        public IQuestionRepository Question { get; private set; }
        public IQuestionThreadRepository QuestionThread { get; private set; }

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
