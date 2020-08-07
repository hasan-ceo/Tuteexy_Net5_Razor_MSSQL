using System;
using System.Collections.Generic;
using System.Text;
using Tuteexy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tuteexy.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Iron Man
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<userlist> userlist { get; set; }


        //public DbSet<Holiday> Holidays { get; set; }
        //public DbSet<ReportCard> ReportCard { get; set; }
        //public DbSet<randomComp> randomComp { get; set; }

        //

        //public DbSet<ClassRoutine> ClassRoutine { get; set; }
        //public DbSet<ClassRoutineStudent> ClassRoutineStudent { get; set; }
        //public DbSet<Attendance> Attendance { get; set; }



        //LMS
        public DbSet<School> School { get; set; }
        public DbSet<ClassRoom> ClassRoom { get; set; }
        public DbSet<SchoolTeacher> SchoolTeacher { get; set; }
        public DbSet<ClassRoomStudent> ClassRoomStudent { get; set; }
        public DbSet<Homework> Homework { get; set; }
        public DbSet<HomeworkSheet> HomeworkReply { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SchoolNotice> SchoolNotice { get; set; }
        public DbSet<ClassRoomNotice> ClassRoomNotice { get; set; }
        public DbSet<ClassRoutine> ClassRoutine { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Classwork> Classwork { get; set; }
        public DbSet<Question> Question { get; set; }

        public DbSet<TutorJob> TutorJob { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
    }
}


