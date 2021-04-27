using FitnessAmeera.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FitnessAmeera.Startup))]
namespace FitnessAmeera
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }
        public void CreateDefaultRolesAndUsers()
        {
            var rolemanger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();


            if (!(rolemanger.RoleExists("Admins")))
            {

                role.Name = ("Admins");
                rolemanger.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.Email = "ameera_45149@yahoo.com";
                user.UserName = "ameera_45149@yahoo.com";
                var check = usermanger.Create(user, ")99Ameera");
                if (check.Succeeded)
                {
                    usermanger.AddToRole(user.Id, "Admins");
                }
            }
            if (!(rolemanger.RoleExists("SuperUsers")))
            {

                role.Name = ("SuperUsers");
                rolemanger.Create(role);
            }
            if (!(rolemanger.RoleExists("Users")))
            {

                role.Name = ("Users");
                rolemanger.Create(role);
            }

        }
    }
}
