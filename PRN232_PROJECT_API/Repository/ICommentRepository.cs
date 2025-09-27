using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> GetByContentAsync(string content);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment entity);
        Task UpdateAsync(Comment entity);
        Task DeleteAsync(int id);
    }
}
