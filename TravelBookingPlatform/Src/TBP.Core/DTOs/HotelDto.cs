using TBP.Domain.Entites;

namespace TBP.Core.DTOs
{
    public class HotelDto
    {
        public string Name { get; set; }
        public int? StarRating { get; set; }
        public string Location { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string Owner { get; set; }
        public ICollection<RoomDto> Rooms { get; set; }

    }
}
