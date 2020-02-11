using Microsoft.AspNetCore.Http;
using System;

namespace Reportably.Services.Models
{
    public class Report
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Author { get; set; }

        public IFormFile File { get; set; }

        public ulong DownloadCount { get; set; }
    }
}
