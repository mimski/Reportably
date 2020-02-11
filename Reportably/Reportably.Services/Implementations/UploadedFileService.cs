using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reportably.Entities;
using Reportably.Services.Contracts;
using Reportably.Services.Mappings;
using Reportably.Services.Models;
using Reportably.Web.Data;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Reportably.Services.Implementations
{
    public class UploadedFileService : IUploadedFileService
    {
        private readonly ReportablyDbContext context;

        public UploadedFileService(ReportablyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAsync(IFormFile uploadedFile, Guid reportId, CancellationToken cancellationToken)
        {
            UploadedFileEntity uploadedFileEntity;
            string content;
            if (uploadedFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    uploadedFile.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    content = Convert.ToBase64String(fileBytes);
                    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(uploadedFile.ContentDisposition);
                    var array = parsedContentDisposition.FileName.Split('\\');
                    string lastElement = array[array.Length - 1];
                    var name = lastElement.Substring(0, lastElement.Length - 1);
                    // act on the Base64 data
                    uploadedFileEntity = new UploadedFileEntity
                    {
                        FileContent = fileBytes,
                        Id = new Guid(),
                        Name = name,
                        ReportId = reportId,
                        Lenght = uploadedFile.Length, 
                        ContentType = uploadedFile.ContentType,
                    };
                }
            }
            else
            {
                return false;
            }
            this.context.UploadedFiles.Add(uploadedFileEntity);
            await this.context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<UploadedFile> GetFileAsync(Guid reportId, CancellationToken cancellationToken)
        {
            var file = await this.context.UploadedFiles.AsNoTracking().FirstOrDefaultAsync(r => r.ReportId == reportId, cancellationToken);
           
            return file.ToService();
        }
    }
}
