using FastEndpoints;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.HotelCommands
{
    public class GetHotels
    {
        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IHotelRepository _dataService;

            public CommandHandler(IHotelRepository dataService)
            {
                _dataService = dataService;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var hotels = await _dataService.GetHotelsAsync();

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

        public class Command : ICommand<Response>
        {
        }

        public class Response
        {
            public IEnumerable<Hotel> Hotels { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}