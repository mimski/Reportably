using System;
using System.ComponentModel.DataAnnotations;

namespace Reportably.Entities
{
    public class ReportEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        public Guid UploadedFile { get; set; }

        public ulong DownloadCount { get; set; }
    }
}
