using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public interface IUserArticleRepository
    {
        Task<IEnumerable<UserArticle>> GetAllAsync();
        Task<UserArticle> GetByIdAsync(string userId, int articleId);
        Task AddAsync(UserArticle entity);
        Task UpdateAsync(UserArticle entity);
        Task DeleteAsync(string userId, int articleId);
        Task<IEnumerable<UserArticle>> GetAllByUserIdAsync(string userId);

    }
}
