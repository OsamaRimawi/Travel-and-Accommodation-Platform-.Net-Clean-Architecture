using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.HotelCommands
{
    public class SearchHotel
    {
        public class Command : ICommand<Response>
        {
            public SearchHotelDto searchHotelDto { get; set; }
        }

        public class Response
        {

            public IEnumerable<Hotel> Hotels { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IHotelRepository _repository;

            public CommandHandler(IHotelRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var hotels = await _repository.SearchAsync(command.searchHotelDto);
                    return new Response
                    {
                        Hotels = hotels
                    };
                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        ErrorMessage = ex.Message
                    };
                }
            }
        }
    }
}