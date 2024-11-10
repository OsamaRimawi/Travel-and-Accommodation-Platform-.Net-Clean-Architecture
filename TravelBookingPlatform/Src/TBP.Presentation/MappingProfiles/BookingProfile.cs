using AutoMapper;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace MappingProfiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDto>();
            CreateMap<BookingDto, Booking>();
        }
    }
}
