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
using System;

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
                    var adminName = "Administrator";
                    var adminEmail = "admin@reportably.com";

                    var exists = await roleManager.RoleExistsAsync(adminName);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = adminName
                        });
                    }

                    var memberName = "Member";

                    var existsMember = await roleManager.RoleExistsAsync(memberName);

                    if (!existsMember)
                    {
                        await roleManager.CreateAsync(new IdentityRole
                        {
                            Name = memberName
                        });
                    }

                    var adminUser = await userManager.FindByIdAsync("61ddd55e-10b9-42e4-a733-fa74e8559679");

                    if (adminUser == null)
                    {
                        var admin1User = new User
                        {
                            Id = "61ddd55e-10b9-42e4-a733-fa74e8559679",
                            EmailConfirmed = true,
                            UserName = adminEmail,
                            Email = adminEmail,
                            SecurityStamp = "4GFB2JI6EFBAAY4BFO7PJM2XRYTIM3GP",
                            ConcurrencyStamp = "a8d70f77-66e0-479e-aa4c-3167d542fcfc",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@REPORTABLY.COM"

                        };

                        await userManager.CreateAsync(admin1User, "Admin100!");
                        await userManager.AddToRoleAsync(admin1User, adminName);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }
        }

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
