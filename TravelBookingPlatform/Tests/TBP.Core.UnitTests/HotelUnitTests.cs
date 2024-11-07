using Moq;
using TBP.Core.CommandHandlers.HotelCommands;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using TBP.Core.DTOs;
using AutoMapper;
using Xunit;

namespace TBP.Core.UnitTests
{
    public class HotelUnitTests
    {
        public class GetHotelsUnitTests
        {
            private readonly Mock<IHotelRepository> _dataServiceMock;
            private readonly GetHotels.CommandHandler _handler;

            public GetHotelsUnitTests()
            {
                _dataServiceMock = new Mock<IHotelRepository>();
                _handler = new GetHotels.CommandHandler(_dataServiceMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_HotelsFound_ReturnsSuccessResponse()
            {
                // Arrange
                var hotels = new List<Hotel>
                {
                    new Hotel { Name = "Hotel1" },
                    new Hotel { Name = "Hotel2" }
                };
                _dataServiceMock.Setup(ds => ds.GetHotelsAsync()).ReturnsAsync(hotels);
                var command = new GetHotels.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Hotels);
                Assert.Equal(2, response.Hotels.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoHotelsFound_ReturnsEmptyResponse()
            {
                // Arrange
                _dataServiceMock.Setup(ds => ds.GetHotelsAsync()).ReturnsAsync(new List<Hotel>());
                var command = new GetHotels.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.Hotels);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                _dataServiceMock.Setup(ds => ds.GetHotelsAsync()).ThrowsAsync(new Exception("Service error"));
                var command = new GetHotels.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotels);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class GetHotelUnitTests
        {
            private readonly Mock<IHotelRepository> _repositoryMock;
            private readonly GetHotel.CommandHandler _handler;

            public GetHotelUnitTests()
            {
                _repositoryMock = new Mock<IHotelRepository>();
                _handler = new GetHotel.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_HotelFound_ReturnsSuccessResponse()
            {
                // Arrange
                var hotelId = 1;
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotel);
                var command = new GetHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Hotel);
                Assert.Equal("Sample Hotel", response.Hotel.Name);
            }

            [Fact]
            public async Task ExecuteAsync_HotelNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var hotelId = 1;
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync((Hotel)null);
                var command = new GetHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Hotel not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var hotelId = 1;
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ThrowsAsync(new Exception("Service error"));
                var command = new GetHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class CreateHotelUnitTests
        {
            private readonly Mock<IHotelRepository> _repositoryMock;
            private readonly CreateHotel.CommandHandler _handler;
            private readonly IMapper _mapper;

            public CreateHotelUnitTests()
            {
                _repositoryMock = new Mock<IHotelRepository>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<HotelDto, Hotel>();
                });
                _mapper = config.CreateMapper();

                _handler = new CreateHotel.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_HotelCreated_ReturnsSuccessResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.AddHotelAsync(It.IsAny<Hotel>())).ReturnsAsync(hotel);
                var command = new CreateHotel.Command { hotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Hotel);
                Assert.Equal("Sample Hotel", response.Hotel.Name);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.AddHotelAsync(It.IsAny<Hotel>())).ThrowsAsync(new Exception("Service error"));
                var command = new CreateHotel.Command { hotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class UpdateHotelUnitTests
        {
            private readonly Mock<IHotelRepository> _repositoryMock;
            private readonly UpdateHotel.CommandHandler _handler;
            private readonly IMapper _mapper;

            public UpdateHotelUnitTests()
            {
                _repositoryMock = new Mock<IHotelRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<HotelDto, Hotel>();
                });
                _mapper = config.CreateMapper();

                _handler = new UpdateHotel.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_HotelUpdated_ReturnsSuccessResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(It.IsAny<int>())).ReturnsAsync(hotel);
                _repositoryMock.Setup(repo => repo.UpdateHotelAsync(It.IsAny<int>(), It.IsAny<Hotel>())).ReturnsAsync(hotel);
                var command = new UpdateHotel.Command { Id = 1, HotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Hotel);
                Assert.Equal("Sample Hotel", response.Hotel.Name);
            }

            [Fact]
            public async Task ExecuteAsync_HotelNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(It.IsAny<int>())).ReturnsAsync((Hotel)null);
                var command = new UpdateHotel.Command { Id = 1, HotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Hotel not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateHotel.Command { Id = 1, HotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Service error", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileUpdating_ReturnsErrorResponse()
            {
                // Arrange
                var hotelDto = new HotelDto { Name = "Sample Hotel" };
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(It.IsAny<int>())).ReturnsAsync(hotel);
                _repositoryMock.Setup(repo => repo.UpdateHotelAsync(It.IsAny<int>(), It.IsAny<Hotel>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateHotel.Command { Id = 1, HotelDto = hotelDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotel);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class DeleteHotelUnitTests
        {
            private readonly Mock<IHotelRepository> _repositoryMock;
            private readonly DeleteHotel.CommandHandler _handler;

            public DeleteHotelUnitTests()
            {
                _repositoryMock = new Mock<IHotelRepository>();
                _handler = new DeleteHotel.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_HotelDeleted_ReturnsSuccessResponse()
            {
                // Arrange
                var hotelId = 1;
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotel);
                _repositoryMock.Setup(repo => repo.DeleteHotelAsync(hotelId)).Returns(Task.CompletedTask);
                var command = new DeleteHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_HotelNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var hotelId = 1;
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync((Hotel)null);
                var command = new DeleteHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Hotel not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var hotelId = 1;
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ThrowsAsync(new Exception("Service error"));
                var command = new DeleteHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileDeleting_ReturnsErrorResponse()
            {
                var hotelId = 1;
                var hotel = new Hotel { Name = "Sample Hotel" };
                _repositoryMock.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync(hotel);
                _repositoryMock.Setup(repo => repo.DeleteHotelAsync(hotelId)).ThrowsAsync(new Exception("Service error"));
                var command = new DeleteHotel.Command { Id = hotelId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);

            }
        }
        public class SearchHotelUnitTests
        {
            private readonly Mock<IHotelRepository> _repositoryMock;
            private readonly SearchHotel.CommandHandler _handler;

            public SearchHotelUnitTests()
            {
                _repositoryMock = new Mock<IHotelRepository>();
                _handler = new SearchHotel.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_HotelsFound_ReturnsSuccessResponse()
            {
                // Arrange
                var searchCriteria = new SearchHotelDto { /* set search criteria properties */ };
                var hotels = new List<Hotel>
        {
            new Hotel { Name = "Hotel1" },
            new Hotel { Name = "Hotel2" }
        };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ReturnsAsync(hotels);
                var command = new SearchHotel.Command { searchHotelDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Hotels);
                Assert.Equal(2, response.Hotels.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoHotelsFound_ReturnsEmptyResponse()
            {
                // Arrange
                var searchCriteria = new SearchHotelDto { /* set search criteria properties */ };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ReturnsAsync(new List<Hotel>());
                var command = new SearchHotel.Command { searchHotelDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.Hotels);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var searchCriteria = new SearchHotelDto { /* set search criteria properties */ };
                _repositoryMock.Setup(repo => repo.SearchAsync(searchCriteria)).ThrowsAsync(new Exception("Service error"));
                var command = new SearchHotel.Command { searchHotelDto = searchCriteria };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Hotels);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }
    }
}