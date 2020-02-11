using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Reportably.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reportably.Web.Infrastructure;
using Reportably.Entities;
using System;

namespace Reportably.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReportablyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetDefaultConnectionString()));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ReportablyDbContext>();

            services.Configure<IdentityOptions>(options =>
           {
               // Password settings
               options.Password.RequireDigit = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = true;
               options.Password.RequiredLength = 6;
               options.Password.RequiredUniqueChars = 0;

               // Lockout settings
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
               options.Lockout.MaxFailedAccessAttempts = 5;
               options.Lockout.AllowedForNewUsers = true;

               // User settings
               options.User.RequireUniqueEmail = true;
           });

            services.AddBusiness();

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UpdateDatabase();

            app.UseExceptionHandling(env);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints();
        }
    }
}
