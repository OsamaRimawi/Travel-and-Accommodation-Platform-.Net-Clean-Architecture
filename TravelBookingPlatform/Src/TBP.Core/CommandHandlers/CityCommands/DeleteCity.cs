using FastEndpoints;
using TBP.Core.Interfaces;

namespace TBP.Core.CommandHandlers.CityCommands
{
    public class DeleteCity
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
            private readonly ICityRepository _repository;

            public CommandHandler(ICityRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var city = await _repository.GetCityByIdAsync(command.Id);
                    if (city == null)
                    {
                        return new Response { ErrorMessage = "City not found" };
                    }

                    await _repository.DeleteCityAsync(command.Id);
                    return new Response {};
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}