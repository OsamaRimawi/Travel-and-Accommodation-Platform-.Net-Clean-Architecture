using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.CityCommands
{
    public class UpdateCity
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
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
                    var city = await _repository.GetCityByIdAsync(command.Id);
                    if (city == null)
                    {
                        return new Response { ErrorMessage = "City not found" };
                    }
                    var existingId = city.Id; // Store the existing Id
                    city = _mapper.Map<City>(command.cityDto);
                    city.Id = existingId; 
                    city.UpdatedAt = DateTime.UtcNow;

                    var updatedCity = await _repository.UpdateCityAsync(command.Id, city);
                    return new Response { City = updatedCity };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}