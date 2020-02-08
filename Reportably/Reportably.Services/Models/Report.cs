using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reportably.Services.Models
{
    public class Report
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Author { get; set; }

        //[Required]
        //public string UserId { get; set; }

        //public User User { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public IFormFile File { get; set; }

        public long DownloadCount { get; set; }
    }
}
