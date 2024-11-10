using FastEndpoints;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.UserCommands
{
    public class GetUser
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
        }

        public class Response
        {
            public User User { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IUserRepository _repository;

            public CommandHandler(IUserRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var user = await _repository.GetUserByIdAsync(command.Id);
                    if (user == null)
                    {
                        return new Response { ErrorMessage = "User not found" };
                    }

                    return new Response { User = user };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}