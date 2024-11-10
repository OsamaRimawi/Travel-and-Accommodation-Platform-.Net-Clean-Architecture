using TBP.Domain.Entites;

namespace TBP.Core.DTOs
{
    public class CityDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string PostOffice { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public ICollection<HotelDto> Hotels { get; set; }
    }
}
