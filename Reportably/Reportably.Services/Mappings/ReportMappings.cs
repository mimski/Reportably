using Reportably.Entities;
using Reportably.Services.Mappings.Extensions;
using Reportably.Services.Models;
using System.Collections.Generic;

namespace Reportably.Services.Mappings
{
    internal static class ReportMappings
    {
        public static Report ToService(this ReportEntity entity)
        {
            return entity != null
                ? new Report
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Summary = entity.Summary,
                    Author = entity.Author,
                    PublicationDate = entity.PublicationDate,
                    DownloadCount = entity.DownloadCount
                }
                : null;
        }

        public static ReportEntity ToEntity(this Report model)
        {
            return model != null
                ? new ReportEntity
                {
                    Id = model.Id,
                    Title = model.Title,
                    Summary = model.Summary,
                    Author = model.Author,
                    PublicationDate = model.PublicationDate,
                    DownloadCount = model.DownloadCount,
                }
                : null;
        }

        public static IReadOnlyCollection<Report> ToService(this IReadOnlyCollection<ReportEntity> entities)
            => entities.MapCollection(ToService);
    }
}
