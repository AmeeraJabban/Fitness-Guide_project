using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace FitnessAmeera.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Age { get; internal set; }
        public string Sex { get; internal set; }
        public int Weight { get; internal set; }
        public int Heigh { get; internal set; }
        public string Status { get; internal set; }
        public virtual ICollection<Articles> Articles { get; set; }
        public virtual ICollection<FitnessTips> FitnessTips { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
                                       //connection to database
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Database.CommandTimeout = 120;
        }
                                                      // DbSet
        public static ApplicationDbContext Create()
        {
           

            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FitnessAmeera.Models.ArticalesTypes> ArticalesTypes { get; set; }


        public System.Data.Entity.DbSet<FitnessAmeera.Models.Articles> Articles { get; set; }


        public System.Data.Entity.DbSet<FitnessAmeera.Models.FitnessTips> FitnessTips { get; set; }

        public System.Data.Entity.DbSet<FitnessAmeera.Models.MySavedArticles> MySavedArticles { get; set; }

    }
}