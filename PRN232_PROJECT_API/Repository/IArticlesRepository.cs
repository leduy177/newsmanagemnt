using PRN232_PROJECT_API.DTO;
using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository.IRepository
{
    public interface IArticlesRepository
    {
        Task<List<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task DeleteAsync(Article article);
    }
    
}
