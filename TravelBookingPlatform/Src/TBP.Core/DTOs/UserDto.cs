namespace TBP.Core.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public ICollection<BookingDto>? Bookings { get; set; }

    }
}
