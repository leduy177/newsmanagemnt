using System.ComponentModel.DataAnnotations;

namespace PRN232_PROJECT_API.DTO
{
    public class ArticleCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required, MaxLength(5000)]
        public string Content { get; set; }

        public string Source { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, MaxLength(1000)]
        public string ImageUrl { get; set; }
        public List<int> TagIds { get; set; }
    }
}
