using FastEndpoints;
using TBP.Core.DTOs;

namespace GetAll
{
    internal sealed class Response
    {
        public IEnumerable<CityDto> Cities { get; set; } = new List<CityDto>();
        public string ErrorMessage { get; set; }
    }
}
