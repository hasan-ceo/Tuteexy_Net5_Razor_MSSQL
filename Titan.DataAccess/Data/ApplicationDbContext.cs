using System;
using System.Collections.Generic;
using System.Text;
using Titan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Titan.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Iron Man
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<userlist> userlist { get; set; }


        //public DbSet<Holiday> Holidays { get; set; }
        //public DbSet<ReportCard> ReportCard { get; set; }
        //public DbSet<randomComp> randomComp { get; set; }

        //public DbSet<NoticeBoard> NoticeBoard { get; set; }

        //public DbSet<ClassRoutine> ClassRoutine { get; set; }
        //public DbSet<ClassRoutineStudent> ClassRoutineStudent { get; set; }
        //public DbSet<Attendance> Attendance { get; set; }



        //LMS
        public DbSet<School> Schools { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<SchoolTeacher> SchoolTeachers { get; set; }
        public DbSet<ClassRoomStudent> ClassRoomStudents { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}


