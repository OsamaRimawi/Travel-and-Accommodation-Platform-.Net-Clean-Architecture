using FastEndpoints;
using TBP.Core.Interfaces;

namespace TBP.Core.CommandHandlers.FeaturedDealCommands
{
    public class DeleteFeaturedDeal
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
                        return new Response { ErrorMessage = "Featured Deal not found" };
                    }
                    await _repository.DeleteFeaturedDealAsync(command.Id);
                    return new Response();
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}