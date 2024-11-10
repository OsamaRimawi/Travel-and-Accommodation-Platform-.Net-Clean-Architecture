using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Update
{
    internal sealed class Mapper : Mapper<Request, Response, object>
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
