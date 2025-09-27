using System.ComponentModel.DataAnnotations;

namespace PRN232_PROJECT_API.Model
{
    public class Comment
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
