using System.ComponentModel.DataAnnotations;

namespace PRN232_PROJECT_API.Model
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required, MaxLength(5000)]
        public string Content { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required, MaxLength(1000)]
        public string ImageUrl { get; set; }
        // Navigation
        public ICollection<UserArticle> UserArticles { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

}
