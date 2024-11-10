using FastEndpoints;
using TBP.Core.Interfaces;

namespace TBP.Core.CommandHandlers.RoomCommands
{
    public class DeleteRoom
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
            private readonly IRoomRepository _repository;

            public CommandHandler(IRoomRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var room = await _repository.GetRoomByIdAsync(command.Id);
                    if (room == null)
                    {
                        return new Response { ErrorMessage = "Room not found" };
                    }

                    await _repository.DeleteRoomAsync(command.Id);
                    return new Response { };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}