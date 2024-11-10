using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.UserEndpoints.Book
{
    internal sealed class Mapper : Mapper<Request, Response, Booking>
    {
        private readonly AutoMapper.IMapper _mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public Response FromEntity(Booking booking)
        {
            if (booking == null)
            {
                return new Response { }; // Return an empty
            }
            var bookingDto = _mapper.Map<BookingDto>(booking);
            return new Response { Booking = bookingDto };
        }
    }
}
