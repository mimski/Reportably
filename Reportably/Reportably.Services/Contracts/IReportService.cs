using Reportably.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Contracts
{
    public interface IReportService
    {
        Task<Report> AddAsync(Report report, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Report>> GetAllAsync(CancellationToken cancellationToken);

        Task<Report> FindByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> UpdateDownloadCountAsync(Guid reportId, CancellationToken cancellationToken);
    }
}
