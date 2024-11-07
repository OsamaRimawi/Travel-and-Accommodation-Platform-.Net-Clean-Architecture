using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.HotelCommands
{
    public class CreateHotel
    {
        public class Command : ICommand<Response>
        {
            public HotelDto hotelDto { get; set; }
        }

        public class Response
        {
            public Hotel Hotel { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IHotelRepository _repository;
            private readonly IMapper _mapper;


            public CommandHandler(IHotelRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var hotel = _mapper.Map<Hotel>(command.hotelDto);
                    hotel.CreatedAt = DateTime.UtcNow;
                    var createdHotel = await _repository.AddHotelAsync(hotel);

                    return new Response { Hotel = createdHotel };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}