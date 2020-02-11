using Microsoft.EntityFrameworkCore;
using Reportably.Entities;
using Reportably.Services.Contracts;
using Reportably.Services.Mappings;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ReportSystem> GetTotalReportsbCountAsync(CancellationToken cancellationToken)
        {
            var query = from reports in this.context.Reports

                        select reports.Id;
            var result = await query.CountAsync(cancellationToken);
            
            var reportSystem = new ReportSystem
            {
               TotalReports = result
            };

            return reportSystem;
        }

        public async Task<IReadOnlyCollection<Report>> Search(string title, string summary, string author, string option)
        {
            if (option.Equals("and"))
            {
                IQueryable<ReportEntity> query = this.context.Set<ReportEntity>();

                if (title != null)
                {
                    query = query.Where(report => report.Title.ToLower().Contains(title.ToLower()));
                }

                if (summary != null)
                {
                    query = query.Where(report => report.Summary.ToLower().Contains(summary.ToLower()));
                }

                if (author != null)
                {
                    query = query.Where(report => report.Author.ToLower().Contains(author.ToLower()));
                }

                var result = await query.ToListAsync();
                return result.ToService();
            }
            else if (option.Equals("or"))
            {
                HashSet<ReportEntity> hashBook = new HashSet<ReportEntity>();
                if (title != null)
                {
                    var reportsByTitle = await this.context.Reports.Where(report => report.Title.Contains(title)).ToListAsync();
                    foreach (var reportByTitle in reportsByTitle)
                    {
                        hashBook.Add(reportByTitle);
                    }
                }

                if (summary != null)
                {
                    var Summaries = await this.context.Reports.Where(report => report.Summary.Contains(summary)).ToListAsync();
                    foreach (var reportSummary in Summaries)
                    {
                        hashBook.Add(reportSummary);
                    }
                }
                if (author != null)
                {
                    var reportsByAuthor = await this.context.Reports.Where(report => report.Author.Contains(author)).ToListAsync();

                    foreach (var reportByAuthor in reportsByAuthor)
                    {
                        hashBook.Add(reportByAuthor);
                    }
                }
                return hashBook.ToService();
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
