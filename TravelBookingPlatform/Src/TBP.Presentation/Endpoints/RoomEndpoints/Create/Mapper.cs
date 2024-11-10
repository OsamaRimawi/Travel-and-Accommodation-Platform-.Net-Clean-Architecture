using FastEndpoints;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Create
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
            return new Response { Room = room };
        }
    }
}
