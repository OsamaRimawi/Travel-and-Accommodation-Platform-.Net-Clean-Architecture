using FastEndpoints;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.FeaturedDealCommands
{
    public class GetFeaturedDeals
    {
        public class Command : ICommand<Response>
        {
        }

        public class Response
        {
            public IEnumerable<FeaturedDeal> FeaturedDeals { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IFeaturedDealRepository _repository;

            public CommandHandler(IFeaturedDealRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var featuredDeals = await _repository.GetFeaturedDealsAsync();
                    return new Response { FeaturedDeals = featuredDeals };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}