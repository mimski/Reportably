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
    }
}
