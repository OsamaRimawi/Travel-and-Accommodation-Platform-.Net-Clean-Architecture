using Moq;
using TBP.Core.CommandHandlers.FeaturedDealCommands;
using TBP.Core.Interfaces;
using TBP.Core.DTOs;
using AutoMapper;
using TBP.Domain.Entites;

namespace TBP.Core.UnitTests
{
    public class FeaturedDealUnitTests
    {
        public class CreateFeaturedDealUnitTests
        {
            private readonly Mock<IFeaturedDealRepository> _repositoryMock;
            private readonly CreateFeaturedDeal.CommandHandler _handler;
            private readonly IMapper _mapper;

            public CreateFeaturedDealUnitTests()
            {
                _repositoryMock = new Mock<IFeaturedDealRepository>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeaturedDealDto, FeaturedDeal>();
                });
                _mapper = config.CreateMapper();

                _handler = new CreateFeaturedDeal.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealCreated_ReturnsSuccessResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.AddFeaturedDealAsync(It.IsAny<FeaturedDeal>())).ReturnsAsync(featuredDeal);
                var command = new CreateFeaturedDeal.Command { FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.FeaturedDeal);
                Assert.Equal(1, response.FeaturedDeal.RoomId);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.AddFeaturedDealAsync(It.IsAny<FeaturedDeal>())).ThrowsAsync(new Exception("Service error"));
                var command = new CreateFeaturedDeal.Command { FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class GetFeaturedDealsUnitTests
        {
            private readonly Mock<IFeaturedDealRepository> _repositoryMock;
            private readonly GetFeaturedDeals.CommandHandler _handler;

            public GetFeaturedDealsUnitTests()
            {
                _repositoryMock = new Mock<IFeaturedDealRepository>();
                _handler = new GetFeaturedDeals.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealsFound_ReturnsSuccessResponse()
            {
                // Arrange
                var featuredDeals = new List<FeaturedDeal>
                {
                    new FeaturedDeal { RoomId = 1 },
                    new FeaturedDeal { RoomId = 2 }
                };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealsAsync()).ReturnsAsync(featuredDeals);
                var command = new GetFeaturedDeals.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.FeaturedDeals);
                Assert.Equal(2, response.FeaturedDeals.Count());
            }

            [Fact]
            public async Task ExecuteAsync_NoFeaturedDealsFound_ReturnsEmptyResponse()
            {
                // Arrange
                _repositoryMock.Setup(repo => repo.GetFeaturedDealsAsync()).ReturnsAsync(new List<FeaturedDeal>());
                var command = new GetFeaturedDeals.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Empty(response.FeaturedDeals);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                _repositoryMock.Setup(repo => repo.GetFeaturedDealsAsync()).ThrowsAsync(new Exception("Service error"));
                var command = new GetFeaturedDeals.Command();

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeals);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class GetFeaturedDealUnitTests
        {
            private readonly Mock<IFeaturedDealRepository> _repositoryMock;
            private readonly GetFeaturedDeal.CommandHandler _handler;

            public GetFeaturedDealUnitTests()
            {
                _repositoryMock = new Mock<IFeaturedDealRepository>();
                _handler = new GetFeaturedDeal.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealFound_ReturnsSuccessResponse()
            {
                // Arrange
                var featuredDealId = 1;
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ReturnsAsync(featuredDeal);
                var command = new GetFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.FeaturedDeal);
                Assert.Equal(1, response.FeaturedDeal.RoomId);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealId = 1;
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ReturnsAsync((FeaturedDeal)null);
                var command = new GetFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Featured deal not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealId = 1;
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ThrowsAsync(new Exception("Service error"));
                var command = new GetFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class UpdateFeaturedDealUnitTests
        {
            private readonly Mock<IFeaturedDealRepository> _repositoryMock;
            private readonly UpdateFeaturedDeal.CommandHandler _handler;
            private readonly IMapper _mapper;

            public UpdateFeaturedDealUnitTests()
            {
                _repositoryMock = new Mock<IFeaturedDealRepository>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FeaturedDealDto, FeaturedDeal>();
                });
                _mapper = config.CreateMapper();

                _handler = new UpdateFeaturedDeal.CommandHandler(_repositoryMock.Object, _mapper);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealUpdated_ReturnsSuccessResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(It.IsAny<int>())).ReturnsAsync(featuredDeal);
                _repositoryMock.Setup(repo => repo.UpdateFeaturedDealAsync(It.IsAny<int>(), It.IsAny<FeaturedDeal>())).ReturnsAsync(featuredDeal);
                var command = new UpdateFeaturedDeal.Command { Id = 1, FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.NotNull(response.FeaturedDeal);
                Assert.Equal(1, response.FeaturedDeal.RoomId);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(It.IsAny<int>())).ReturnsAsync((FeaturedDeal)null);
                var command = new UpdateFeaturedDeal.Command { Id = 1, FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Featured deal not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateFeaturedDeal.Command { Id = 1, FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Service error", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsExceptionWhileUpdating_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealDto = new FeaturedDealDto { RoomId = 1 };
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(It.IsAny<int>())).ReturnsAsync(featuredDeal);
                _repositoryMock.Setup(repo => repo.UpdateFeaturedDealAsync(It.IsAny<int>(), It.IsAny<FeaturedDeal>())).ThrowsAsync(new Exception("Service error"));
                var command = new UpdateFeaturedDeal.Command { Id = 1, FeaturedDealDto = featuredDealDto };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.FeaturedDeal);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }

        public class DeleteFeaturedDealUnitTests
        {
            private readonly Mock<IFeaturedDealRepository> _repositoryMock;
            private readonly DeleteFeaturedDeal.CommandHandler _handler;

            public DeleteFeaturedDealUnitTests()
            {
                _repositoryMock = new Mock<IFeaturedDealRepository>();
                _handler = new DeleteFeaturedDeal.CommandHandler(_repositoryMock.Object);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealDeleted_ReturnsSuccessResponse()
            {
                // Arrange
                var featuredDealId = 1;
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ReturnsAsync(featuredDeal);
                _repositoryMock.Setup(repo => repo.DeleteFeaturedDealAsync(featuredDealId)).Returns(Task.CompletedTask);
                var command = new DeleteFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Null(response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_FeaturedDealNotFound_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealId = 1;
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ReturnsAsync((FeaturedDeal)null);
                var command = new DeleteFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Featured Deal not found", response.ErrorMessage);
            }

            [Fact]
            public async Task ExecuteAsync_DataServiceThrowsException_ReturnsErrorResponse()
            {
                // Arrange
                var featuredDealId = 1;
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ThrowsAsync(new Exception("Service error"));
                var command = new DeleteFeaturedDeal.Command { Id = featuredDealId };

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
                var featuredDealId = 1;
                var featuredDeal = new FeaturedDeal { RoomId = 1 };
                _repositoryMock.Setup(repo => repo.GetFeaturedDealByIdAsync(featuredDealId)).ReturnsAsync(featuredDeal);
                _repositoryMock.Setup(repo => repo.DeleteFeaturedDealAsync(featuredDealId)).Throws(new Exception("Service error"));
                var command = new DeleteFeaturedDeal.Command { Id = featuredDealId };

                // Act
                var response = await _handler.ExecuteAsync(command, CancellationToken.None);

                // Assert
                Assert.NotNull(response);
                Assert.Equal("Service error", response.ErrorMessage);
            }
        }
    }
}