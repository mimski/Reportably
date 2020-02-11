using Reportably.Services.Models;
using Reportably.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Reportably.Web.Mappings
{
    internal static class UploadedFileMappings
    {
        public static UploadedFileViewModel ToViewModel(this UploadedFile entity)
        {
            return entity != null ? new UploadedFileViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                FileContent = entity.FileContent,
                ContentType = entity.ContentType,
                Lenght = entity.Lenght,
                ReportId = entity.ReportId
            } : null;
        }

        public static UploadedFile ToServiceModel(this UploadedFileViewModel viewModel)
        {
            return viewModel != null ? new UploadedFile
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                FileContent = viewModel.FileContent,
                ContentType = viewModel.ContentType,
                Lenght = viewModel.Lenght,
                ReportId = viewModel.ReportId
            } : null;
        }

        public static IReadOnlyCollection<UploadedFileViewModel> ToViewModel(this IReadOnlyCollection<UploadedFile> entities)
        {
            if (entities.Count == 0)
            {
                return Array.Empty<UploadedFileViewModel>();
            }

            var files = new UploadedFileViewModel[entities.Count];

            var index = 0;

            foreach (var entity in entities)
            {
                files[index] = entity.ToViewModel();
                ++index;
            }

            return new ReadOnlyCollection<UploadedFileViewModel>(files);
        }
    }
}
