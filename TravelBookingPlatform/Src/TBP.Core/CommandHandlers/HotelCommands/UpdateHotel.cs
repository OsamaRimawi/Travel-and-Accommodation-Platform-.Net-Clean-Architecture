using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.HotelCommands
{
    public class UpdateHotel
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
            public HotelDto HotelDto { get; set; }
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
                    var hotel = await _repository.GetHotelByIdAsync(command.Id);
                    if (hotel == null)
                    {
                        return new Response { ErrorMessage = "Hotel not found" };
                    }
                    var existingId = hotel.Id; // Store the existing Id
                    hotel = _mapper.Map<Hotel>(command.HotelDto);
                    hotel.Id = existingId;
                    hotel.UpdatedAt = DateTime.UtcNow;

                    var updatedHotel = await _repository.UpdateHotelAsync(command.Id, hotel);
                    return new Response { Hotel = updatedHotel };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}