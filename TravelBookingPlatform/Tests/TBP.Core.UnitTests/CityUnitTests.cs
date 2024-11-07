using Moq;
using TBP.Core.CommandHandlers.CityCommands;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using TBP.Core.DTOs;
using AutoMapper;

namespace TBP.Core.UnitTests
{
    public class CityUnitTests
    {
        public class GetCitiesUnitTests
        {
            private readonly Mock<ICityRepository> _dataServiceMock;
            private readonly GetCities.CommandHandler _handler;

            public GetCitiesUnitTests()
            {
                _dataServiceMock = new Mock<ICityRepository>();
                _handler = new GetCities.CommandHandler(_dataServiceMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_CitiesFound_ReturnsSuccessResponse()
            {
                // Arrange
                var cities = new List<City>
            {
                new City { Name = "City1" },
                new City { Name = "City2" }
            };
                _dataServiceMock.Setup(ds => ds.GetCitiesAsync()).ReturnsAsync(cities);
                var command = new GetCities.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.Cities);
                Assert.Equal(2, response.Cities.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoCitiesFound_ReturnsEmptyResponse()
            {
                // Arrange
                _dataServiceMock.Setup(ds => ds.GetCitiesAsync()).ReturnsAsync(new List<City>());
                var command = new GetCities.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.Cities);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                _dataServiceMock.Setup(ds => ds.GetCitiesAsync()).ThrowsAsync(new Exception("Service error"));
                var command = new GetCities.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.Cities);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class GetCityUnitTests
        {
            private readonly Mock<ICityRepository> _repositoryMock;
            private readonly GetCity.CommandHandler _handler;

            public GetCityUnitTests()
            {
                _repositoryMock = new Mock<ICityRepository>();
                _handler = new GetCity.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_CityFound_ReturnsSuccessResponse()
            {
                // Arrange
                var cityId = 1;
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync(city);
                var command = new GetCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.City);
                Assert.Equal("Sample City", response.City.Name);
            }

            [Fact]
            public async Task ExecuteAsync_CityNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var cityId = 1;
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync((City)null);
                var command = new GetCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("City not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var cityId = 1;
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ThrowsAsync(new Exception("Service error"));
                var command = new GetCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class CreateCityUnitTests
        {
            private readonly Mock<ICityRepository> _repositoryMock;
            private readonly CreateCity.CommandHandler _handler;
            private readonly IMapper _mapper;


            public CreateCityUnitTests()
            {
                _repositoryMock = new Mock<ICityRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CityDto, City>();
                });
                _mapper = config.CreateMapper();

                _handler = new CreateCity.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_CityCreated_ReturnsSuccessResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.AddCityAsync(It.IsAny<City>())).ReturnsAsync(city);
                var command = new CreateCity.Command { cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.City);
                Assert.Equal("Sample City", response.City.Name);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.AddCityAsync(It.IsAny<City>())).ThrowsAsync(new Exception("Service error"));
                var command = new CreateCity.Command { cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class UpdateCityUnitTests
        {
            private readonly Mock<ICityRepository> _repositoryMock;
            private readonly UpdateCity.CommandHandler _handler;
            private readonly IMapper _mapper;

            public UpdateCityUnitTests()
            {
                _repositoryMock = new Mock<ICityRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CityDto, City>();
                });
                _mapper = config.CreateMapper();

                _handler = new UpdateCity.CommandHandler(_repositoryMock.Object, _mapper);
            }
            [Fact]
            public async Task ExecuteAsync_CityUpdated_ReturnsSuccessResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(It.IsAny<int>())).ReturnsAsync(city);
                _repositoryMock.Setup(repo => repo.UpdateCityAsync(It.IsAny<int>(), It.IsAny<City>())).ReturnsAsync(city);
                var command = new UpdateCity.Command { Id = 1, cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.City);
                Assert.Equal("Sample City", response.City.Name);
            }
            [Fact]
            public async Task ExecuteAsync_CityNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(It.IsAny<int>())).ReturnsAsync((City)null);
                var command = new UpdateCity.Command { Id = 1, cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("City not found", response.ErrorMessage);
            }
            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateCity.Command { Id = 1, cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("Service error", response.ErrorMessage);
            }
            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileUpdating_ReturnsErrorResponse()
            {
                // Arrange
                var cityDto = new CityDto { Name = "Sample City" };
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(It.IsAny<int>())).ReturnsAsync(city);
                _repositoryMock.Setup(repo => repo.UpdateCityAsync(It.IsAny<int>(), It.IsAny<City>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateCity.Command { Id = 1, cityDto = cityDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.City);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class DeleteCityUnitTests
        {
            private readonly Mock<ICityRepository> _repositoryMock;
            private readonly DeleteCity.CommandHandler _handler;

            public DeleteCityUnitTests()
            {
                _repositoryMock = new Mock<ICityRepository>();
                _handler = new DeleteCity.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_CityDeleted_ReturnsSuccessResponse()
            {
                // Arrange
                var cityId = 1;
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync(city);
                _repositoryMock.Setup(repo => repo.DeleteCityAsync(cityId)).Returns(Task.CompletedTask);
                var command = new DeleteCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_CityNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var cityId = 1;
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync((City)null);
                var command = new DeleteCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("City not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var cityId = 1;
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ThrowsAsync(new Exception("Service error"));
                var command = new DeleteCity.Command { Id = cityId };

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
                var cityId = 1;
                var city = new City { Name = "Sample City" };
                _repositoryMock.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync(city);
                _repositoryMock.Setup(repo => repo.DeleteCityAsync(cityId)).Throws(new Exception("Service error"));
                var command = new DeleteCity.Command { Id = cityId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);

            }
        }
    }
}