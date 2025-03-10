using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Utitlity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBooks.DataAccess.DbInitializer
{
    


    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
             RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager; //for adding roles
            _db = db;   //for adding company list to the registration page
        }

        public void Initialize()
        {

            //migration if they are not applied

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                { 
                  _db.Database.Migrate();
                
                
                }

            }
            catch (Exception ex)
            {

                throw;
            }


            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();



                //if roles are not created, we will create admin user as well

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "adminYaseen@gmail.com",
                    Email = "adminYaseen@gmail.com",
                    Name = "adminYaseen",
                    PhoneNumber = "03339485834",
                    StreetAddress = "test 123 pak",
                    State = "KPK",
                    PostalCode = "28100",
                    City = "Bannu"
                }, "Khan@1122").GetAwaiter().GetResult();

                //we use .GetAwaiter ().GetResult() to make it synchronous, otherwise it will be async and we will not be able to get the result

                //assign the admin user to the admin role, so for that we need to get the user by email

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "adminYaseen@gmail.com");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }
            //steps for seeding database is :
            //1. create a class that would seed the database
            //2. create a method in that class that would seed the database
            //3. call that method in the program.cs file
            //4. run the application
            //5. check the database if the data is seeded or not
            



            return;

        }

    }
}
