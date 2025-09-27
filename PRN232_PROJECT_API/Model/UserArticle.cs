namespace PRN232_PROJECT_API.Model
{
    public class UserArticle
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public string RoleInArticle { get; set; }
    }
}
