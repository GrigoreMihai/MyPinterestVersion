﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyPinterestVersion.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyPinterestVersion.Startup))]
namespace MyPinterestVersion
{
    public partial class Startup
    {
        private void createAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new
            RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new
            UserStore<ApplicationUser>(context));
            // Se adauga rolurile aplicatiei
            if (!roleManager.RoleExists("Administrator"))
            {
                // Se adauga rolul de administrator
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                var adminCreated = UserManager.Create(user, "Administrator1!");
                if (adminCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Administrator");
                }
            }
            if (!roleManager.RoleExists("RegisteredUser"))
            {
                var role = new IdentityRole();
                role.Name = "RegisteredUser";
                roleManager.Create(role);
            }         
        }
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);

            createAdminUserAndApplicationRoles();
        }
    }
}
