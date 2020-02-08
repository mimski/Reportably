using Reportably.Entities;
using Reportably.Services.Mappings.Extensions;
using Reportably.Services.Models;
using System.Collections.Generic;

namespace Reportably.Services.Mappings
{
    internal static class UploadedFileMapping
    {
        public static UploadedFile ToService(this UploadedFileEntity entity)
        {
            return entity != null
                ? new UploadedFile
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    FileContent = entity.FileContent,
                    ContentType = entity.ContentType,
                    Lenght = entity.Lenght,
                    ReportId = entity.ReportId
                }
                : null;
        }

        public static UploadedFileEntity ToEntity(this UploadedFile model)
        {
            return model != null
                ? new UploadedFileEntity
                {
                    Id = model.Id,
                    Name = model.Name,
                    FileContent = model.FileContent,
                    ContentType = model.ContentType,
                    Lenght = model.Lenght,
                    ReportId = model.ReportId
                }
                : null;
        }

        public static IReadOnlyCollection<UploadedFile> ToService(this IReadOnlyCollection<UploadedFileEntity> entities)
            => entities.MapCollection(ToService);
    }
}
