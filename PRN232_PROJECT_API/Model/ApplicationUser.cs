using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PRN232_PROJECT_API.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FullName { get; set; }

        // Navigation
        public ICollection<UserArticle> UserArticles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
