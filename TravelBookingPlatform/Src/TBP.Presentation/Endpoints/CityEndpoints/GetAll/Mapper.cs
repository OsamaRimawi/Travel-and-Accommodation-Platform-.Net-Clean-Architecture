using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;
using System.Collections.Generic;

namespace GetAll
{
    internal sealed class Mapper : Mapper<EmptyRequest, Response, object>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(IEnumerable<City> cities)
        {
            if (cities == null)
            {
                return new Response { Cities = new List<CityDto>() }; // Return an empty list
            }

            var cityDtos = _mapper.Map<IEnumerable<CityDto>>(cities); // Direct mapping
            return new Response { Cities = cityDtos };
        }
    }
}
