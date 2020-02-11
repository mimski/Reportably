using Reportably.Services.Models;
using Reportably.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Reportably.Web.Areas.Reports.Mappings
{
    internal static class ReportMappings
    {
        public static ReportViewModel ToViewModel(this Report entity)
        {
            return entity != null ? new ReportViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Summary = entity.Summary,
                Author = entity.Author,
                PublicationDate = entity.PublicationDate,
                File = entity.File,
                DownloadCount = entity.DownloadCount

            } : null;
        }

        public static Report ToServiceModel(this ReportViewModel viewModel)
        {
            return viewModel != null ? new Report
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Summary = viewModel.Summary,
                Author = viewModel.Author,
                PublicationDate = viewModel.PublicationDate,
                File = viewModel.File,
                DownloadCount = viewModel.DownloadCount,
            } : null;
        }

        public static IReadOnlyCollection<ReportViewModel> ToViewModel(this IReadOnlyCollection<Report> entities)
        {
            if (entities.Count == 0)
            {
                return Array.Empty<ReportViewModel>();
            }

            var reports = new ReportViewModel[entities.Count];

            var index = 0;

            foreach (var entity in entities)
            {
                reports[index] = entity.ToViewModel();
                ++index;
            }

            return new ReadOnlyCollection<ReportViewModel>(reports);
        }
    }
}
