using PRN232_PROJECT_API.Model;

namespace PRN232_PROJECT_API.Repository
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task<Tag> GetByNameAsync(string name);
        Task AddAsync(Tag entity);
        Task UpdateAsync(Tag entity);
        Task DeleteAsync(int id);
    }

}
