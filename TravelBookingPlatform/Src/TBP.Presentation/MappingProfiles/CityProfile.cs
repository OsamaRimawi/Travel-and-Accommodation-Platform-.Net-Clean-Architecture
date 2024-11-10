using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace MappingProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
        }
    }
}
