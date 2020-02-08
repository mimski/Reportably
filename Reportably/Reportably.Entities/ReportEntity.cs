using Reportably.Entities.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Reportably.Entities
{
    public class ReportEntity : IAuditable, IDeletable
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

        //[Required]
        //public string UserId { get; set; }

        //public User User { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool IsDeleted { get ; set; }
       
        public DateTime? DeletedOn { get; set; }

        public Guid UploadedFile { get; set; }

        public long DownloadCount { get; set; }
    }
}
