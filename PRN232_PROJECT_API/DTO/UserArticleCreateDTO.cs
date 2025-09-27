using System.Text.Json.Serialization;

namespace PRN232_PROJECT_API.DTO
{
    public class UserArticleCreateDTO
    {
        public string UserId { get; set; }     
        public int ArticleId { get; set; }
        public string RoleInArticle { get; set; }
    }
}
