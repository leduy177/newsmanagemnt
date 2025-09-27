using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(int id);
    }
}
