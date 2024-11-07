using FastEndpoints;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.HotelEndpoints.GetAll
{
    internal sealed class Response
    {
        public IEnumerable<HotelDto> Hotels { get; set; } = new List<HotelDto>();
        public string ErrorMessage { get; set; }
    }
}
