namespace RentApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RentApp.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RentApp.Persistance.RADBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RentApp.Persistance.RADBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            /*      if (!context.Roles.Any(r => r.Name == "Admin"))
                  {
                      var store = new RoleStore<IdentityRole>(context);
                      var manager = new RoleManager<IdentityRole>(store);
                      var role = new IdentityRole { Name = "Admin" };

                      manager.Create(role);
                  }

                  if (!context.Roles.Any(r => r.Name == "Manager"))
                  {
                      var store = new RoleStore<IdentityRole>(context);
                      var manager = new RoleManager<IdentityRole>(store);
                      var role = new IdentityRole { Name = "Manager" };

                      manager.Create(role);
                  }

                  if (!context.Roles.Any(r => r.Name == "AppUser"))
                  {
                      var store = new RoleStore<IdentityRole>(context);
                      var manager = new RoleManager<IdentityRole>(store);
                      var role = new IdentityRole { Name = "AppUser" };

                      manager.Create(role);
                  }


                  context.SaveChanges();

                  context.AppUsers.AddOrUpdate(u => u.Id, new AppUser() { Id = 1, FullName = "Admin Adminovic", FirstName = "Admin", LastName = "Adminovic", DateOfBirth = DateTime.Now });


                  context.SaveChanges();

                  var userStore = new UserStore<RAIdentityUser>(context);
                  var userManager = new UserManager<RAIdentityUser>(userStore);

                  if (!context.Users.Any(u => u.UserName == "admin"))
                  {
                      var _appUser = context.AppUsers.FirstOrDefault(a => a.FullName == "Admin Adminovic");
                      var user = new RAIdentityUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = RAIdentityUser.HashPassword("admin"), AppUserId = _appUser.Id };
                      userManager.Create(user);
                      userManager.AddToRole(user.Id, "Admin");
                  }*/
        }
    }
}
