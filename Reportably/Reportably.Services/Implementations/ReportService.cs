using Microsoft.EntityFrameworkCore;
using Reportably.Services.Contracts;
using Reportably.Services.Mappings;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly ReportablyDbContext context;

        public ReportService(ReportablyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Report> AddAsync(Report report, CancellationToken cancellationToken)
        {
            var addedReport = this.context.Reports.Add(report.ToEntity());
            await this.context.SaveChangesAsync(cancellationToken);
            return addedReport.Entity.ToService();
        }

        public async Task<IReadOnlyCollection<Report>> GetAllAsync(CancellationToken cancellationToken)
        {
            var reports = await this.context.Reports.AsNoTracking().ToListAsync(cancellationToken);
            return reports.ToService();
        }

        public async Task<Report> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var report = await this.context.Reports.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

            return report.ToService();
        }

        public async Task<bool> UpdateDownloadCountAsync(Guid reportId, CancellationToken cancellationToken)
        {
            var existingReport = await this.context.Reports.FirstOrDefaultAsync(report => report.Id == reportId, cancellationToken);
            if (existingReport != null)
            {
                existingReport.DownloadCount += 1;

                this.context.Reports.Update(existingReport);
                await this.context.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
