using EVideoGameStoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EVideoGameStoreApp.Data.Services
{
    public class DevelopersService : IDevelopersService
    {
        private readonly AppDbContext _context;
        public DevelopersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Developer developer)
        {
            await _context.Developers.AddAsync(developer);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var result = await _context.Developers.FirstOrDefaultAsync(n => n.Id == id);
            _context.Developers.Remove(result);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Developer>> GetAllAsync()
        {
            return await _context.Developers.ToListAsync();
        }

        public async Task<Developer?> GetByIdAsync(int id)
        {
            return await _context.Developers.FirstOrDefaultAsync(d => d.Id == id);
        }



        public async Task<Developer> UpdateAsync(int id, Developer newDeveloper)
        {
            var existingDeveloper = await _context.Developers.FirstOrDefaultAsync(d => d.Id == id);

            if(existingDeveloper != null)
            {
                existingDeveloper.Name = newDeveloper.Name;
                existingDeveloper.Country = newDeveloper.Country;
                existingDeveloper.Description = newDeveloper.Description;
                existingDeveloper.LogoUrl = newDeveloper.LogoUrl;

                await _context.SaveChangesAsync();
            }
            
            return newDeveloper;
        }
    }
}
