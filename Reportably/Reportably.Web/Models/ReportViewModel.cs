using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Reportably.Web.Models
{
    public class ReportViewModel
    {
        public Guid Id { get; set; }

     
        [MaxLength(255, ErrorMessage = "Title must be less than 256 symbols.")]
        [Required(ErrorMessage = "Please enter a title for this report.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please select a publication date.")]
        [Display(Name = "Publication Date")]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        //[Required]
        //public string UserId { get; set; }

        //public User User { get; set; }

        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Modified On")]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Deleted On")]
        [DataType(DataType.Date)]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Deleted")]
        [DataType(DataType.Date)]
        public bool IsDeleted { get; set; }

        public IFormFile File { get; set; }

        public long DownloadCount { get; set; }
    }
}
