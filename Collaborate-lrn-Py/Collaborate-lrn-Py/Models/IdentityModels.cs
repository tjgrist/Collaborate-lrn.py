using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Collaborate_lrn_Py.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection<Path> Paths { get; set; }
        public virtual ICollection<CollaborativeTutorial> Collaborations { get; set; }
        public int Points { get; set; }

        [Display(Name = "Completed Tutorials")]
        public int CompletedTutorialsCount { get; set; }

        //public List<Tutorials> UpvotedTutorials { get; set; }

    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Path> Paths { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<CollaborativeTutorial> CollaborativeTutorials { get; set; }

    }
}