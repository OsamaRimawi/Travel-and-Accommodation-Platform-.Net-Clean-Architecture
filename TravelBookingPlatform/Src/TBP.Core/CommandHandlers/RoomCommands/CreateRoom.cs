using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.RoomCommands
{
    public class CreateRoom
    {
        public class Command : ICommand<Response>
        {
            public RoomDto RoomDto { get; set; }
        }

        public class Response
        {
            public Room Room { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IRoomRepository _repository;
            private readonly IMapper _mapper;

            public CommandHandler(IRoomRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var room = _mapper.Map<Room>(command.RoomDto);
                    room.CreatedAt = DateTime.UtcNow;
                    var createdRoom = await _repository.AddRoomAsync(room);

                    return new Response { Room = createdRoom };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}