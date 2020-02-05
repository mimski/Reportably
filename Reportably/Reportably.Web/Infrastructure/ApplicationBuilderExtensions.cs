using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Reportably.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Reportably.Entities;
using Reportably.Web.Utils;

namespace Reportably.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            return app;
        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
        => app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
              );

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapRazorPages();
        });

        //public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        //   => app.SeedDataAsync().GetAwaiter().GetResult();


        //public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.CreateScope())
        //    {
        //        var services = serviceScope.ServiceProvider;
        //        var dbContext = services.GetService<ReportablyDbContext>();

        //        await dbContext.Database.MigrateAsync();

        //        var roleManager = services.GetService<RoleManager<IdentityRole>>();
        //        var existingRole = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRole);
        //        if (existingRole != null)
        //        {
        //            return app;
        //        }

        //        var adminRole = new IdentityRole(GlobalConstants.AdministratorRole);

        //        await roleManager.CreateAsync(adminRole);

        //        var adminUser = new User
        //        {
        //            UserName = "admin@reportably.com",
        //            Email = "admin@reportably.com",
        //            SecurityStamp = "RandomSecurityStamp"
        //        };

        //        var userManager = services.GetService<UserManager<User>>();

        //        await userManager.CreateAsync(adminUser, "adminpass");

        //        await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRole);

        //        var user = new User
        //        {
        //            UserName = "normal@reportably.com",
        //            Email = "normal@reportably.com",
        //            SecurityStamp = "AnotherRandomSecurityStamp"
        //        };

        //        await userManager.CreateAsync(user, "password");

        //        await dbContext.SaveChangesAsync();
        //    }

        //    return app;
        //}
    }
}
