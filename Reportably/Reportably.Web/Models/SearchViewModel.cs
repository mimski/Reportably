using System.ComponentModel.DataAnnotations;

namespace Reportably.Web.Models
{
    public class SearchViewModel
    {
        [MaxLength(255, ErrorMessage = "Title must be less than 256 symbols.")]
        public string Title { get; set; }

        [MaxLength(255, ErrorMessage = "Title must be less than 256 symbols.")]
        public string Summary { get; set; }

        [MaxLength(255, ErrorMessage = "Title must be less than 256 symbols.")]
        public string Author { get; set; }

        [MaxLength(3, ErrorMessage = "Please enter and/or words for options.")]
        public string Option { get; set; }
    }
}
