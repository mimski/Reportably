using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reportably.Entities;
using System.Reflection;

namespace Reportably.Web.Data
{
    public class ReportablyDbContext : IdentityDbContext<User>
    {
        public ReportablyDbContext(DbContextOptions<ReportablyDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReportEntity> Reports { get; set; }

        public DbSet<UploadedFileEntity> UploadedFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
