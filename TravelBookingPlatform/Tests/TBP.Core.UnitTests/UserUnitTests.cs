using AutoMapper;
using Moq;
using TBP.Core.CommandHandlers.UserCommands;
using TBP.Core.DTOs;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using Xunit;

namespace TBP.Core.UnitTests
{
    public class UserUnitTests
    {
        public class GetUserUnitTests
        {
            private readonly Mock<IUserRepository> _userRepositoryMock;
            private readonly GetUser.CommandHandler _commandHandler;

            public GetUserUnitTests()
            {
                _userRepositoryMock = new Mock<IUserRepository>();
                _commandHandler = new GetUser.CommandHandler(_userRepositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_UserExists_ReturnsUser()
            {
                // Arrange
                var userId = 1;
                var user = new User { Id = userId };
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                    .ReturnsAsync(user);

                var command = new GetUser.Command { Id = userId };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.User);
                Assert.Equal(userId, result.User.Id);
                Assert.Null(result.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_UserDoesNotExist_ReturnsErrorMessage()
            {
                // Arrange
                var userId = 1;
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                    .ReturnsAsync((User)null);

                var command = new GetUser.Command { Id = userId };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.User);
                Assert.Equal("User not found", result.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_ExceptionThrown_ReturnsErrorMessage()
            {
                // Arrange
                var userId = 1;
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                    .ThrowsAsync(new Exception("Service error"));

                var command = new GetUser.Command { Id = userId };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.User);
                Assert.Equal("Service error", result.ErrorMessage);
            }
        }

        public class CreateBookingUnitTests
        {
            private readonly Mock<IUserRepository> _userRepositoryMock;
            private readonly Mock<IRoomRepository> _roomRepositoryMock;
            private readonly Mock<IMapper> _mapperMock;
            private readonly CreateBooking.CommandHandler _commandHandler;

            public CreateBookingUnitTests()
            {
                _userRepositoryMock = new Mock<IUserRepository>();
                _roomRepositoryMock = new Mock<IRoomRepository>();
                _mapperMock = new Mock<IMapper>();
                _commandHandler = new CreateBooking.CommandHandler(_userRepositoryMock.Object, _roomRepositoryMock.Object, _mapperMock.Object);
            }

            [Fact]
            public async Task HandleAsync_RoomExistsAndUserExists_ReturnsBooking()
            {
                // Arrange
                var bookingDto = new BookingDto { RoomId = 1, UserId = 1 };
                var room = new Room { Id = 1, Availability = true, Price = 100 };
                var user = new User { Id = 1 };
                var booking = new Booking { Id = 1, RoomId = 1, UserId = 1 };

                _roomRepositoryMock.Setup(repo => repo.GetRoomByIdAsync(bookingDto.RoomId))
                    .ReturnsAsync(room);
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(bookingDto.UserId))
                    .ReturnsAsync(user);
                _mapperMock.Setup(mapper => mapper.Map<Booking>(bookingDto))
                    .Returns(booking);
                _userRepositoryMock.Setup(repo => repo.CreateBookingForUserAsync(It.IsAny<Booking>()))
                    .ReturnsAsync(booking);

                var command = new CreateBooking.Command { BookingDto = bookingDto };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Booking);
                Assert.Equal(booking.Id, result.Booking.Id);
                Assert.Null(result.ErrorMessage);
            }

            [Fact]
            public async Task HandleAsync_RoomDoesNotExist_ReturnsErrorMessage()
            {
                // Arrange
                var bookingDto = new BookingDto { RoomId = 1, UserId = 1 };

                _roomRepositoryMock.Setup(repo => repo.GetRoomByIdAsync(bookingDto.RoomId))
                    .ReturnsAsync((Room)null);

                var command = new CreateBooking.Command { BookingDto = bookingDto };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.Booking);
                Assert.Equal("Room not found", result.ErrorMessage);
            }

            [Fact]
            public async Task HandleAsync_UserDoesNotExist_ReturnsErrorMessage()
            {
                // Arrange
                var bookingDto = new BookingDto { RoomId = 1, UserId = 1 };
                var room = new Room { Id = 1, Availability = true };

                _roomRepositoryMock.Setup(repo => repo.GetRoomByIdAsync(bookingDto.RoomId))
                    .ReturnsAsync(room);
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(bookingDto.UserId))
                    .ReturnsAsync((User)null);

                var command = new CreateBooking.Command { BookingDto = bookingDto };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.Booking);
                Assert.Equal("User not found", result.ErrorMessage);
            }

            [Fact]
            public async Task HandleAsync_RoomNotAvailable_ReturnsErrorMessage()
            {
                // Arrange
                var bookingDto = new BookingDto { RoomId = 1, UserId = 1 };
                var room = new Room { Id = 1, Availability = false };
                var user = new User { Id = 1 };

                _roomRepositoryMock.Setup(repo => repo.GetRoomByIdAsync(bookingDto.RoomId))
                    .ReturnsAsync(room);
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(bookingDto.UserId))
                    .ReturnsAsync(user);


                var command = new CreateBooking.Command { BookingDto = bookingDto };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.Booking);
                Assert.Equal("Room is not available right now", result.ErrorMessage);
            }

            [Fact]
            public async Task HandleAsync_ExceptionThrown_ReturnsErrorMessage()
            {
                // Arrange
                var bookingDto = new BookingDto { RoomId = 1, UserId = 1 };
                var room = new Room { Id = 1, Availability = true, Price = 100 };
                var user = new User { Id = 1 };
                var booking = new Booking { Id = 1, RoomId = 1, UserId = 1 };

                _roomRepositoryMock.Setup(repo => repo.GetRoomByIdAsync(bookingDto.RoomId))
                    .ReturnsAsync(room);
                _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(bookingDto.UserId))
                    .ReturnsAsync(user);
                _mapperMock.Setup(mapper => mapper.Map<Booking>(bookingDto))
                    .Returns(booking);
                _userRepositoryMock.Setup(repo => repo.CreateBookingForUserAsync(It.IsAny<Booking>()))
                    .ThrowsAsync(new Exception("Service Error"));


                var command = new CreateBooking.Command { BookingDto = bookingDto };

                // Act
                var result = await _commandHandler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Null(result.Booking);
                Assert.Equal("Service Error", result.ErrorMessage);
            }
        }
    }
}