using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Get
{
    internal sealed class Mapper : Mapper<EmptyRequest, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(Hotel hotel)
        {
            if (hotel == null)
            {
                return new Response {}; // Return an empty
            }
            var hotelDto = _mapper.Map<HotelDto>(hotel);
            return new Response { Hotel = hotelDto };
        }
    }
}
