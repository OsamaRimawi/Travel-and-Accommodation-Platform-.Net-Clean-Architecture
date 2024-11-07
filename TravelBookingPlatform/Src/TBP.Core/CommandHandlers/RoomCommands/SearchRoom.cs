using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.RoomCommands
{
    public class SearchRoom
    {
        public class Command : ICommand<Response>
        {
            public SearchRoomDto SearchRoomDto { get; set; }
        }

        public class Response
        {
            public IEnumerable<Room> Rooms { get; set; }
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
                    var rooms = await _repository.SearchAsync(command.SearchRoomDto);
                    return new Response
                    {
                        Rooms = rooms
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
    }
}