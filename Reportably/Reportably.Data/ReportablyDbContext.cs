using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ReportEntity>()
            //    .HasOne(user => user.User)
            //    .WithMany(user => user.Reports)
            //    .HasForeignKey(user => user.UserId);

            base.OnModelCreating(builder);
        }
    }
}
