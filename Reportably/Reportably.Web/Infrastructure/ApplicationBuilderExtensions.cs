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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapRazorPages();
        });

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ReportablyDbContext>();
                context.Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var exists = await roleManager.RoleExistsAsync(GlobalConstants.AdministratorRole);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = GlobalConstants.AdministratorRole
                        });
                    }

                    var adminUser = await userManager.FindByIdAsync(GlobalConstants.AdminId);

                    if (adminUser == null)
                    {
                        var admin1User = new User
                        {
                            Id = GlobalConstants.AdminId,
                            UserName = GlobalConstants.AdminEmail,
                            Email = GlobalConstants.AdminEmail,
                            NormalizedEmail = GlobalConstants.NormalizedEmail,
                            EmailConfirmed = true,
                            SecurityStamp = GlobalConstants.SecurityStamp,
                            ConcurrencyStamp = GlobalConstants.ConcurrencyStamp,
                            LockoutEnabled = false,
                        };

                        await userManager.CreateAsync(admin1User, GlobalConstants.AdminPassword);
                        await userManager.AddToRoleAsync(admin1User, GlobalConstants.AdministratorRole);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}
