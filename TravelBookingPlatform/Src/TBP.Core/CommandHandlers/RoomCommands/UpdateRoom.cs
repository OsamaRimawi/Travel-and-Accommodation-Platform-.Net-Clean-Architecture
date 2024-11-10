using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.RoomCommands
{
    public class UpdateRoom
    {
        public class Command : ICommand<Response>
        {
            public int Id { get; set; }
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
                    var room = await _repository.GetRoomByIdAsync(command.Id);
                    if (room == null)
                    {
                        return new Response { ErrorMessage = "Room not found" };
                    }
                    var existingId = room.Id; // Store the existing Id
                    room = _mapper.Map<Room>(command.RoomDto);
                    room.Id = existingId;
                    room.UpdatedAt = DateTime.UtcNow;

                    var updatedRoom = await _repository.UpdateRoomAsync(command.Id, room);
                    return new Response { Room = updatedRoom };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}