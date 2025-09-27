namespace PROJECT_CLIENT.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }

        public string? ArticleTitle { get; set; }
        public string? UserFullName { get; set; }
    }
}
