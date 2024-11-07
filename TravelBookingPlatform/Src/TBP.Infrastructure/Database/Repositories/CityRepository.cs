using Microsoft.EntityFrameworkCore;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Infrastructure.Database.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly TBPDbContext _context;

        public CityRepository(TBPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities
                .Include(c => c.Hotels)
                    .ThenInclude(h => h.Rooms)
                .ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _context.Cities
                .Include(c => c.Hotels)
                    .ThenInclude(h => h.Rooms)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<City> AddCityAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task<City> UpdateCityAsync(int id, City city)
        {
            var existingCity = await _context.Cities.FindAsync(id);
            if (existingCity == null)
            {
                return null;
            }

            _context.Entry(existingCity).CurrentValues.SetValues(city);
            await _context.SaveChangesAsync();
            return existingCity;
        }

        public async Task DeleteCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
    }
}