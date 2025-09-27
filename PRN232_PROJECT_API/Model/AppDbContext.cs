using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRN232_PROJECT_API.Model;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ArticleTag> ArticleTags { get; set; }
    public DbSet<UserArticle> UserArticles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Composite keys for n-n relationships
        builder.Entity<ArticleTag>().HasKey(at => new { at.ArticleId, at.TagId });
        builder.Entity<UserArticle>().HasKey(ua => new { ua.UserId, ua.ArticleId });

       

        // Seed Categories
        builder.Entity<Category>().HasData(
            Enumerable.Range(1, 15).Select(i => new Category
            {
                Id = i,
                Name = $"Category {i}"
            })
        );

        // Seed Tags
        builder.Entity<Tag>().HasData(
            Enumerable.Range(1, 15).Select(i => new Tag
            {
                Id = i,
                Name = $"Tag {i}"
            })
        );

        // Seed Users
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var users = Enumerable.Range(1, 15).Select(i =>
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = $"user{i}@mail.com",
                NormalizedUserName = $"USER{i}@MAIL.COM",
                Email = $"user{i}@mail.com",
                NormalizedEmail = $"USER{i}@MAIL.COM",
                EmailConfirmed = true,
                FullName = $"User {i}",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Password123!");
            return user;
        }).ToList();

        builder.Entity<ApplicationUser>().HasData(users);

        // Seed Articles
        var articles = Enumerable.Range(1, 15).Select(i => new Article
        {
            Id = i,
            Title = $"Article Title {i}",
            Content = $"Sample content for article {i}",
            CreatedAt = DateTime.UtcNow.AddDays(-i),
            CategoryId = (i % 15) + 1,
            Status = 1,
            ImageUrl="none",
            Source="VN"
        }).ToList();
        builder.Entity<Article>().HasData(articles);

        // Seed UserArticle
        var userArticles = users.Take(10).Select((user, index) => new UserArticle
        {
            ArticleId = (index % 15) + 1,
            UserId = user.Id,
            RoleInArticle = "MainAuthor"
        });
        builder.Entity<UserArticle>().HasData(userArticles);

        // Seed ArticleTag
        var articleTags = Enumerable.Range(1, 15).Select(i => new ArticleTag
        {
            ArticleId = i,
            TagId = (i % 15) + 1
        });
        builder.Entity<ArticleTag>().HasData(articleTags);

        // Seed Comments
        var comments = Enumerable.Range(1, 15).Select(i => new Comment
        {
            Id = i,
            Content = $"Comment {i} content.",
            PostedAt = DateTime.UtcNow,
            ArticleId = (i % 15) + 1,
            UserId = users[i % users.Count].Id
        });
        builder.Entity<Comment>().HasData(comments);
    }
}
