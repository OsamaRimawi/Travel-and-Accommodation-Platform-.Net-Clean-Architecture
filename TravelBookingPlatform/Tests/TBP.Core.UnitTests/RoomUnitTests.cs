using Moq;
using TBP.Core.CommandHandlers.RoomCommands;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using TBP.Core.DTOs;
using AutoMapper;
using Xunit;

namespace TBP.Core.UnitTests
{
    public class RoomUnitTests
    {
        public class CreateRoomUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly CreateRoom.CommandHandler _handler;
            private readonly IMapper _mapper;

            public CreateRoomUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RoomDto, Room>();
                });
                _mapper = config.CreateMapper();

                _handler = new CreateRoom.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_RoomCreated_ReturnsSuccessResponse()
            {
                // Arrange
                var roomDto = new RoomDto {Number = 1 };
                var room = new Room { Number = 1 };
                _repositoryMock.Setup(repo => repo.AddRoomAsync(It.IsAny<Room>())).ReturnsAsync(room);
                var command = new CreateRoom.Command { RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Room);
                Assert.Equal(1, response.Room.Number);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var roomDto = new RoomDto { Number = 1 };
                _repositoryMock.Setup(repo => repo.AddRoomAsync(It.IsAny<Room>())).ThrowsAsync(new Exception("Service error"));
                var command = new CreateRoom.Command { RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class GetRoomsUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly GetRooms.CommandHandler _handler;

            public GetRoomsUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();
                _handler = new GetRooms.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_RoomsFound_ReturnsSuccessResponse()
            {
                // Arrange
                var rooms = new List<Room>
        {
            new Room { Number = 1 },
            new Room { Number = 2 }
        };
                _repositoryMock.Setup(repo => repo.GetRoomsAsync()).ReturnsAsync(rooms);
                var command = new GetRooms.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Rooms);
                Assert.Equal(2, response.Rooms.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoRoomsFound_ReturnsEmptyResponse()
            {
                // Arrange
                _repositoryMock.Setup(repo => repo.GetRoomsAsync()).ReturnsAsync(new List<Room>());
                var command = new GetRooms.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.Rooms);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                _repositoryMock.Setup(repo => repo.GetRoomsAsync()).ThrowsAsync(new Exception("Service error"));
                var command = new GetRooms.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Rooms);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }
        public class GetRoomUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly GetRoom.CommandHandler _handler;

            public GetRoomUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();
                _handler = new GetRoom.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_RoomFound_ReturnsSuccessResponse()
            {
                // Arrange
                var roomId = 1;
                var room = new Room {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync(room);
                var command = new GetRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Room);
                Assert.Equal(1, response.Room.Number);
            }

            [Fact]
            public async Task ExecuteAsync_RoomNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var roomId = 1;
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync((Room)null);
                var command = new GetRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Room not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var roomId = 1;
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ThrowsAsync(new Exception("Service error"));
                var command = new GetRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class UpdateRoomUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly UpdateRoom.CommandHandler _handler;
            private readonly IMapper _mapper;

            public UpdateRoomUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RoomDto, Room>();
                });
                _mapper = config.CreateMapper();

                _handler = new UpdateRoom.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_RoomUpdated_ReturnsSuccessResponse()
            {
                // Arrange
                var roomDto = new RoomDto {Number = 1 };
                var room = new Room {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(It.IsAny<int>())).ReturnsAsync(room);
                _repositoryMock.Setup(repo => repo.UpdateRoomAsync(It.IsAny<int>(), It.IsAny<Room>())).ReturnsAsync(room);
                var command = new UpdateRoom.Command { Id = 1, RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Room);
                Assert.Equal(1, response.Room.Number);
            }

            [Fact]
            public async Task ExecuteAsync_RoomNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var roomDto = new RoomDto {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(It.IsAny<int>())).ReturnsAsync((Room)null);
                var command = new UpdateRoom.Command { Id = 1, RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Room not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var roomDto = new RoomDto {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateRoom.Command { Id = 1, RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Service error", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileUpdating_ReturnsErrorResponse()
            {
                // Arrange
                var roomDto = new RoomDto {Number = 1 };
                var room = new Room {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(It.IsAny<int>())).ReturnsAsync(room);
                _repositoryMock.Setup(repo => repo.UpdateRoomAsync(It.IsAny<int>(), It.IsAny<Room>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateRoom.Command { Id = 1, RoomDto = roomDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Room);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class DeleteRoomUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly DeleteRoom.CommandHandler _handler;

            public DeleteRoomUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();
                _handler = new DeleteRoom.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_RoomDeleted_ReturnsSuccessResponse()
            {
                // Arrange
                var roomId = 1;
                var room = new Room {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync(room);
                _repositoryMock.Setup(repo => repo.DeleteRoomAsync(roomId)).Returns(Task.CompletedTask);
                var command = new DeleteRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_RoomNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var roomId = 1;
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync((Room)null);
                var command = new DeleteRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Room not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var roomId = 1;
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ThrowsAsync(new Exception("Service error"));
                var command = new DeleteRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileDeleting_ReturnsErrorResponse()
            {
                // Arrange
                var roomId = 1;
                var room = new Room {Number = 1 };
                _repositoryMock.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync(room);
                _repositoryMock.Setup(repo => repo.DeleteRoomAsync(roomId)).Throws(new Exception("Service error"));
                var command = new DeleteRoom.Command { Id = roomId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class SearchRoomUnitTests
        {
            private readonly Mock<IRoomRepository> _repositoryMock;
            private readonly SearchRoom.CommandHandler _handler;

            public SearchRoomUnitTests()
            {
                _repositoryMock = new Mock<IRoomRepository>();
                _handler = new SearchRoom.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_RoomsFound_ReturnsSuccessResponse()
            {
                // Arrange
                var searchCriteria = new SearchRoomDto { /* set search criteria properties */ };
                var rooms = new List<Room>
                {
                    new Room {Number = 1},
                    new Room {Number = 2}
                };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ReturnsAsync(rooms);
                var command = new SearchRoom.Command { SearchRoomDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Rooms);
                Assert.Equal(2, response.Rooms.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoRoomsFound_ReturnsEmptyResponse()
            {
                // Arrange
                var searchCriteria = new SearchRoomDto { /* set search criteria properties */ };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ReturnsAsync(new List<Room>());
                var command = new SearchRoom.Command { SearchRoomDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.Rooms);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var searchCriteria = new SearchRoomDto { /* set search criteria properties */ };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ThrowsAsync(new Exception("Service error"));
                var command = new SearchRoom.Command { SearchRoomDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Rooms);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }
    }
}