using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.CityCommands
{
    public class CreateCity
    {
        public class Command : ICommand<Response>
        {
            public CityDto cityDto { get; set; }
        }

        public class Response
        {
            public City City { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly ICityRepository _repository;
            private readonly IMapper _mapper;


            public CommandHandler(ICityRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var city = _mapper.Map<City>(command.cityDto);
                    city.CreatedAt = DateTime.UtcNow;
                    var createdCity = await _repository.AddCityAsync(city);

                    return new Response { City = createdCity };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}