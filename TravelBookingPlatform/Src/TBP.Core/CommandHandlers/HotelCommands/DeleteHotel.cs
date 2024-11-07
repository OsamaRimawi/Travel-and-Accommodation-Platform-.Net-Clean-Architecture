using FastEndpoints;
using TBP.Core.Interfaces;

namespace TBP.Core.CommandHandlers.HotelCommands
{
    public class DeleteHotel
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
        }

        public class Response
        {
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
                    var hotel = await _repository.GetHotelByIdAsync(command.Id);
                    if (hotel == null)
                    {
                        return new Response { ErrorMessage = "Hotel not found" };
                    }

                    await _repository.DeleteHotelAsync(command.Id);
                    return new Response { };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}