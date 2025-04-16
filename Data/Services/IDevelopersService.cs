using EVideoGameStoreApp.Models;

namespace EVideoGameStoreApp.Data.Services
{
    public interface IDevelopersService
    {
        Task<IEnumerable<Developer>> GetAllAsync();
        Task <Developer> GetByIdAsync(int id);
        Task AddAsync(Developer developer);
        Task <Developer> UpdateAsync(int id, Developer newDeveloper);
        Task DeleteAsync(int id);
    }
}
