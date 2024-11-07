using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Search
{
    internal sealed class Mapper : Mapper<Request, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(IEnumerable<Hotel> hotels)
        {
            if (hotels == null)
            {
                return new Response { Hotels = new List<HotelDto>() }; // Return an empty list
            }

            var hotelDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels); // Direct mapping
            return new Response { Hotels = hotelDtos };
        }
    }
}
