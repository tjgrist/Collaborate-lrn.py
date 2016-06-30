using Collaborate_lrn_Py.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Collaborate_lrn_Py.Startup))]
namespace Collaborate_lrn_Py
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
  
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
  
            if (!roleManager.RoleExists("Admin"))
            {  
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);               

                var user = new ApplicationUser();
                user.UserName = "SiteAdmin";
                user.Email = "tj@gmail.com";

                string userPWD = "Test#1";

                var chkUser = UserManager.Create(user, userPWD);
 
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }   
            if (!roleManager.RoleExists("Educator"))
            {
                var role = new IdentityRole();
                role.Name = "Educator";
                roleManager.Create(role);
            }    
            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }
        }
    }
}
