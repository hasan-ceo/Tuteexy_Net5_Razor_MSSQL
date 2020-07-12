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
        public DbSet<CoverType> CoverTypes { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<School> Companies { get; set; }


        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<ReportCard> ReportCard { get; set; }
        public DbSet<randomComp> randomComp { get; set; }

        public DbSet<NoticeBoard> NoticeBoard { get; set; }
        public DbSet<userlist> userlist { get; set; }
        public DbSet<ClassRoutine> ClassRoutine { get; set; }
        public DbSet<ClassRoutineStudent> ClassRoutineStudent { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<School> Schools { get; set; }

    }
}


