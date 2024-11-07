using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace MappingProfiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelDto, Hotel>();

        }
    }
}
