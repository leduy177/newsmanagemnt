namespace PRN232_PROJECT_API.DTO
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public List<string> Tags { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public List<UserArticleDTO> UserArticles { get; set; }
    }
}
