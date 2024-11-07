using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.FeaturedDealCommands
{
    public class UpdateFeaturedDeal
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
            public FeaturedDealDto FeaturedDealDto { get; set; }
        }

        public class Response
        {
            public FeaturedDeal FeaturedDeal { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IFeaturedDealRepository _repository;
            private readonly IMapper _mapper;

            public CommandHandler(IFeaturedDealRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var featuredDeal = await _repository.GetFeaturedDealByIdAsync(command.Id);
                    if (featuredDeal == null)
                    {
                        return new Response { ErrorMessage = "Featured deal not found" };
                    }
                    var existingId = featuredDeal.Id; // Store the existing Id
                    featuredDeal = _mapper.Map<FeaturedDeal>(command.FeaturedDealDto);
                    featuredDeal.Id = existingId;
                    featuredDeal.UpdatedAt = DateTime.UtcNow;

                    var updatedFeaturedDeal = await _repository.UpdateFeaturedDealAsync(command.Id, featuredDeal);
                    return new Response { FeaturedDeal = updatedFeaturedDeal };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}