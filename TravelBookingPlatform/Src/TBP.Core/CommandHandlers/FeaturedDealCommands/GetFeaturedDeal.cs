using FastEndpoints;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.FeaturedDealCommands
{
    public class GetFeaturedDeal
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
        }

        public class Response
        {
            public FeaturedDeal FeaturedDeal { get; set; }
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
                    var featuredDeal = await _repository.GetFeaturedDealByIdAsync(command.Id);
                    if (featuredDeal == null)
                    {
                        return new Response { ErrorMessage = "Featured deal not found" };
                    }

                    return new Response { FeaturedDeal = featuredDeal };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}