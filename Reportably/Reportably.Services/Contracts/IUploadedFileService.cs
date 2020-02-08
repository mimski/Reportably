using Microsoft.AspNetCore.Http;
using Reportably.Services.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Contracts
{
    public interface IUploadedFileService
    {
        Task<bool> AddAsync(IFormFile uploadedFile, Guid reportId, CancellationToken cancellationToken);

        Task<UploadedFile> GetFileAsync(Guid reportId, CancellationToken cancellationToken);
    }
}
