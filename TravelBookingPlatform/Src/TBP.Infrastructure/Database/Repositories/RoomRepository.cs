using Microsoft.EntityFrameworkCore;
using TBP.Domain.Entites;
using TBP.Core.Interfaces;
using TBP.Core.DTOs;

namespace TBP.Infrastructure.Database.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TBPDbContext _context;

        public RoomRepository(TBPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Set<Room>()
                                 .Include(r => r.FeaturedDeal)
                                 .ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Set<Room>()
                                 .Include(r => r.FeaturedDeal)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Room> AddRoomAsync(Room room)
        {
            _context.Set<Room>().Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, Room room)
        {
            var existingRoom = await _context.Set<Room>()
                                             .Include(r => r.FeaturedDeal)
                                             .FirstOrDefaultAsync(r => r.Id == id);
            if (existingRoom == null)
            {
                return null;
            }

            _context.Entry(existingRoom).CurrentValues.SetValues(room);

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _context.Set<Room>()
                                     .Include(r => r.FeaturedDeal)
                                     .FirstOrDefaultAsync(r => r.Id == id);
            if (room != null)
            {
                _context.Set<Room>().Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Room>> SearchAsync(SearchRoomDto searchRoomDto)
        {
            var query = _context.Rooms
                       .AsQueryable();

            if (searchRoomDto.Price.HasValue)
            {
                query = query.Where(r => r.Price == searchRoomDto.Price);
            }

            query = query.Where(r =>
                r.AdultCapacity == searchRoomDto.AdultCapacity &&
                r.ChildCapacity == searchRoomDto.ChildCapacity &&
                r.Availability == searchRoomDto.Availability);

            return await query.ToListAsync();
        }
    }
}