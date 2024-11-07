using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
