using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Search
{
    internal sealed class Mapper : Mapper<Request, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(IEnumerable<Room> rooms)
        {
            if (rooms == null)
            {
                return new Response { Rooms = new List<RoomDto>() }; // Return an empty list
            }

            var roomDtos = _mapper.Map<IEnumerable<RoomDto>>(rooms); // Direct mapping
            return new Response { Rooms = roomDtos };
        }
    }
}
