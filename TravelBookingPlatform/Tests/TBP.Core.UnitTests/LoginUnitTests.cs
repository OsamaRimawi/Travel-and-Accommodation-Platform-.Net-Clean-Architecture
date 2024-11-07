using Moq;
using Microsoft.Extensions.Configuration;
using TBP.Core.CommandHandlers.LoginCommands;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using Xunit;

namespace TBP.Core.UnitTests
{
    public class LoginUnitTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Login.CommandHandler _handler;

        public LoginUnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _handler = new Login.CommandHandler(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var user = new User { Username = "testuser", Role = new Role { RoleName = "User" } };
            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAndPasswordAsync("testuser", "password")).ReturnsAsync(user);
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("supersecretkeyForTheUnitTestsBlahBlahBlah");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("issuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("audience");
            var command = new Login.Command { Username = "testuser", Password = "password" };

            // Act
            var response = await _handler.ExecuteAsync(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_InvalidCredentials_ReturnsErrorMessage()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAndPasswordAsync("testuser", "wrongpassword")).ReturnsAsync((User)null);
            var command = new Login.Command { Username = "testuser", Password = "wrongpassword" };

            // Act
            var response = await _handler.ExecuteAsync(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Token);
            Assert.Equal("Invalid username or password", response.ErrorMessage);
        }

        [Fact]
        public async Task ExecuteAsync_UserRepositoryThrowsException_ReturnsErrorMessage()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetUserByUsernameAndPasswordAsync("testuser", "password")).ThrowsAsync(new Exception("Service error"));
            var command = new Login.Command { Username = "testuser", Password = "password" };

            // Act
            var response = await _handler.ExecuteAsync(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Token);
            Assert.Equal("Service error", response.ErrorMessage);
        }
    }
}