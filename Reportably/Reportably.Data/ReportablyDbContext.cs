using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reportably.Data.Configurations;
using Reportably.Entities;

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
            modelBuilder.ApplyConfiguration(new ReportEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
