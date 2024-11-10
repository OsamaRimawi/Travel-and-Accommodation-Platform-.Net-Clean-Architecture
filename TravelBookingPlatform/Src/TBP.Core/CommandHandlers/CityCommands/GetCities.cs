using FastEndpoints;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.CityCommands
{
    public class GetCities
    {
        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly ICityRepository _dataService;

            public CommandHandler(ICityRepository dataService)
            {
                _dataService = dataService;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var cities = await _dataService.GetCitiesAsync();

                    return new Response
                    {
                        Cities = cities
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
            public IEnumerable<City> Cities { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}