using FiveGold.Models.Mapeamento;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FiveGold
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Models.Mapeamento.FiveGoldContext, Migrations.Configuration>());
            ApplicationDbContext db = new ApplicationDbContext();
            CriarRegras(db);
            CriarSuperUsuario(db);
            AddPermissoesSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermissoesSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("fivegoldfinance@hotmail.com");
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if(!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }
            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }
            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }
            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
        }

        private void CriarSuperUsuario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("fivegoldfinance@hotmail.com");

            if(user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "fivegoldfinance@hotmail.com",
                    Email = "fivegoldfinance@hotmail.com",
                    NomeCompleto = "FiveGold-ADM"
                };
                userManager.Create(user, "Five-123");
            }
        }

        private void CriarRegras(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }

            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }

            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
        }
    }
}
