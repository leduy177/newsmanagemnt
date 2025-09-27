using Microsoft.EntityFrameworkCore;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public class UserArticleRepository : IUserArticleRepository
    {
        private readonly AppDbContext _context;

        public UserArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserArticle>> GetAllAsync()
        {
            return await _context.UserArticles
    .Include(u => u.Article)
    .Include(u => u.User) // Assuming there's a navigation property to the User model
    .ToListAsync();

        }

        public async Task<UserArticle?> GetByIdAsync(string userId, int articleId)
        {
            return await _context.UserArticles
                .Include(ua => ua.User) 
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.ArticleId == articleId);
        }


        public async Task AddAsync(UserArticle entity)
        {
            _context.UserArticles.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserArticle entity)
        {
            _context.UserArticles.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId, int articleId)
        {
            var entity = await GetByIdAsync(userId, articleId);
            if (entity != null)
            {
                _context.UserArticles.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<UserArticle>> GetAllByUserIdAsync(string userId)
        {
            return await _context.UserArticles
                .Include(ua => ua.User)
                .Include(ua => ua.Article)
                .Where(ua => ua.UserId == userId)
                .ToListAsync();
        }

    }

}
