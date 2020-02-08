using System;

namespace Reportably.Services.Models
{
    public class UploadedFile
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long Lenght { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        public Guid ReportId { get; set; }
    }
}
