using Reportably.Entities;
using Reportably.Services.Mappings.Extensions;
using Reportably.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
                    CreatedOn = entity.CreatedOn,
                    ModifiedOn = entity.ModifiedOn,
                    DeletedOn = entity.DeletedOn,
                    IsDeleted = entity.IsDeleted
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
                    Author = model.Summary,
                    PublicationDate = model.PublicationDate,
                    CreatedOn = model.CreatedOn,
                    ModifiedOn = model.ModifiedOn,
                    DeletedOn = model.DeletedOn,
                    IsDeleted = model.IsDeleted
                }
                : null;
        }

        public static IReadOnlyCollection<Report> ToService(this IReadOnlyCollection<ReportEntity> entities)
            => entities.MapCollection(ToService);
    }
}
