using TBP.Domain.Entites;

namespace TBP.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task<Booking> CreateBookingForUserAsync(Booking booking);

    }
}
