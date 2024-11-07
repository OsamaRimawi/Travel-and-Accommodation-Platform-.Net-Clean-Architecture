using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;
using System.Collections.Generic;
using TBP.Core.CommandHandlers.CityCommands;

namespace Create
{
    internal sealed class Mapper : Mapper<Request, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(City city)
        {
            if (city == null)
            {
                return new Response {}; // Return an empty
            }
            return new Response { City = city };
        }
    }
}
