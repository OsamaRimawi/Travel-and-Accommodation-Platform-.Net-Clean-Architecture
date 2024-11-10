using AutoMapper;
using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;

namespace TBP.Core.CommandHandlers.UserCommands
{
    public class CreateBooking
    {
        public class Command : ICommand<Response>
        {
            public BookingDto BookingDto { get; set; }
        }

        public class Response
        {
            public Booking Booking { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly IRoomRepository _roomRepository;

            private readonly IMapper _mapper;

            public CommandHandler(IUserRepository repository,IRoomRepository roomRepository, IMapper mapper)
            {
                _userRepository = repository;
                _roomRepository = roomRepository;
                _mapper = mapper;

            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var room = await _roomRepository.GetRoomByIdAsync(command.BookingDto.RoomId);
                    if (room == null)
                    {
                        return new Response { ErrorMessage = "Room not found" };
                    }
                    var user = await _userRepository.GetUserByIdAsync(command.BookingDto.UserId);
                    if (user == null)
                    {
                        return new Response { ErrorMessage = "User not found" };
                    }
                    if(!room.Availability)
                    {
                        return new Response { ErrorMessage = "Room is not available right now" };
                    }
                    var booking = _mapper.Map<Booking>(command.BookingDto);
                    booking.User = user;
                    booking.Room = room;
                    booking.CreatedAt = DateTime.UtcNow;
                    booking.TotalPrice = room.Price;
                    booking.Status = "Booked";
                    room.Availability = false;
                    var createdBooking = await _userRepository.CreateBookingForUserAsync(booking);
                    return new Response { Booking = createdBooking };
                }
                catch (Exception ex)
                {
                    return new Response { ErrorMessage = ex.Message };
                }
            }
        }
    }
}