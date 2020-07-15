using Tuteexy.DataAccess.Data;
using Tuteexy.Models;
using Tuteexy.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tuteexy.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == SD.Role_Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Role_Ironman)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@titan.com",
                Email = "admin@titan.com",
                EmailConfirmed = true,
                Name = "Hasan Habib",
                PhoneNumber = "+8801765263343"
            }, "Admin123!").GetAwaiter().GetResult();

            ApplicationUser user = _db.ApplicationUsers.Where(u => u.Email == "admin@titan.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, SD.Role_Ironman).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "azmainfiaz@gmail.com",
                Email = "azmainfiaz@gmail.com",
                EmailConfirmed = true,
                Name = "Azmain Fiaz",
                PhoneNumber = "+8801765263343"
            }, "Admin123!").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.Where(u => u.Email == "azmainfiaz@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, SD.Role_User).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "bmgigs1@gmail.com",
                Email = "bmgigs1@gmail.com",
                EmailConfirmed = true,
                Name = "GIMSC Online",
                PhoneNumber = "+8801407483604"
            }, "Bmgigs1@").GetAwaiter().GetResult();

            user = _db.ApplicationUsers.Where(u => u.Email == "bmgigs1@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(user, SD.Role_User).GetAwaiter().GetResult();
        }
    }
}
