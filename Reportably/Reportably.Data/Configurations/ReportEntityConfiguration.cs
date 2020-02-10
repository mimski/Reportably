using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reportably.Entities;

namespace Reportably.Data.Configurations
{
    internal class ReportEntityConfiguration : IEntityTypeConfiguration<ReportEntity>
    {
        public void Configure(EntityTypeBuilder<ReportEntity> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(report => report.Id);

            builder.Property(report => report.DownloadCount)
               .HasDefaultValue(0);
        }
    }
}
