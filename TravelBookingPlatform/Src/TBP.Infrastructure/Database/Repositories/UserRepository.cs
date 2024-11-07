using Microsoft.EntityFrameworkCore;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TBPDbContext _context;

        public UserRepository(TBPDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Bookings)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<Booking> CreateBookingForUserAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }
    }
}