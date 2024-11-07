using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.FeaturedDealCommands
{
    public class CreateFeaturedDeal
    {
        public class Command : ICommand<Response>
        {
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
                    var featuredDeal = _mapper.Map<FeaturedDeal>(command.FeaturedDealDto);
                    featuredDeal.CreatedAt = DateTime.UtcNow;
                    var createdFeaturedDeal = await _repository.AddFeaturedDealAsync(featuredDeal);

                    return new Response { FeaturedDeal = createdFeaturedDeal };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}