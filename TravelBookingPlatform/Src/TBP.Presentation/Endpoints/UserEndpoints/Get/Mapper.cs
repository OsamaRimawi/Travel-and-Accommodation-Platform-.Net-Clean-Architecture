using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.UserEndpoints.Get
{
    internal sealed class Mapper : Mapper<Request, Response, Room>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(User user)
        {
            if (user == null)
            {
                return new Response {}; // Return an empty
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new Response { User = userDto };
        }
    }
}
