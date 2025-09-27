using Microsoft.EntityFrameworkCore;
using PRN232_PROJECT_API.Model;
using PRN232_PROJECT_API.Repository.IRepository;

namespace PRN232_PROJECT_API.Repository
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly AppDbContext _context;

        public ArticlesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _context.Articles
                .Include(a => a.Category)
                .Include(a => a.UserArticles).ThenInclude(ua => ua.User)
                .Include(a => a.ArticleTags).ThenInclude(at => at.Tag)
                .Include(a => a.Comments).ThenInclude(c => c.User)
                .ToListAsync();
        }


        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .Include(a => a.Category)
                .Include(a => a.UserArticles)
                    .ThenInclude(ua => ua.User)
                .Include(a => a.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }



        public async Task AddAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Article article)
        {
            article.Status = 0;
            _context.Articles.Update(article); 
            await _context.SaveChangesAsync();
        }

    }
}
