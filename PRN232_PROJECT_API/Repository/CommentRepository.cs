using Microsoft.EntityFrameworkCore;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments
        .Include(c => c.Article)
        .Include(c => c.User)
        .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments
        .Include(c => c.Article)
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<Comment> GetByContentAsync(string content)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Content.ToLower() == content.ToLower());
        }

        public async Task AddAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            _context.Comments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
