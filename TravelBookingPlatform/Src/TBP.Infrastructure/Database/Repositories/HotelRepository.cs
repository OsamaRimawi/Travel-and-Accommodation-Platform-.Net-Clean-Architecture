using Microsoft.EntityFrameworkCore;
using TBP.Domain.Entites;
using TBP.Core.Interfaces;
using TBP.Core.DTOs;

namespace TBP.Infrastructure.Database.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly TBPDbContext _context;

        public HotelRepository(TBPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Set<Hotel>()
                                 .Include(h => h.Rooms)
                                 .ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Set<Hotel>()
                                 .Include(h => h.Rooms)
                                 .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Hotel> AddHotelAsync(Hotel hotel)
        {
            _context.Set<Hotel>().Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> UpdateHotelAsync(int id, Hotel hotel)
        {
            var existingHotel = await _context.Set<Hotel>()
                                              .Include(h => h.Rooms)
                                              .FirstOrDefaultAsync(h => h.Id == id);
            if (existingHotel == null)
            {
                return null;
            }

            _context.Entry(existingHotel).CurrentValues.SetValues(hotel);

            await _context.SaveChangesAsync();
            return existingHotel;
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await _context.Set<Hotel>()
                                      .Include(h => h.Rooms)
                                      .FirstOrDefaultAsync(h => h.Id == id);
            if (hotel != null)
            {
                _context.Set<Hotel>().Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Hotel>> SearchAsync(SearchHotelDto searchHotelDto)
        {
            var query = _context.Hotels
                .Include(h => h.City)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchHotelDto.Name))
            {
                query = query.Where(h => h.Name.Contains(searchHotelDto.Name));
            }

            if (searchHotelDto.StarRating.HasValue)
            {
                query = query.Where(h => h.StarRating == searchHotelDto.StarRating);
            }

            if (!string.IsNullOrEmpty(searchHotelDto.Location))
            {
                query = query.Where(h => h.Location.Contains(searchHotelDto.Location));
            }

            if (!string.IsNullOrEmpty(searchHotelDto.CityName))
            {
                query = query.Where(h => h.City.Name.Contains(searchHotelDto.CityName));
            }

            if (!string.IsNullOrEmpty(searchHotelDto.Owner))
            {
                query = query.Where(h => h.Owner.Contains(searchHotelDto.Owner));
            }

            return await query.ToListAsync();
        }
    }
}