using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, Room>();

        }
    }
}
