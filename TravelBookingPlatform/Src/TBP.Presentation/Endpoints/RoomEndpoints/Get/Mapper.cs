using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Get
{
    internal sealed class Mapper : Mapper<Request, Response, Room>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(Room room)
        {
            if (room == null)
            {
                return new Response {}; // Return an empty
            }
            var roomDto = _mapper.Map<RoomDto>(room);
            return new Response { Room = roomDto };
        }
    }
}
