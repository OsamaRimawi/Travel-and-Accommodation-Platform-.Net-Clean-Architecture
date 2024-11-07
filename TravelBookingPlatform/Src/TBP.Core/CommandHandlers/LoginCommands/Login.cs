using FastEndpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TBP.Core.Interfaces;
using TBP.Domain.Entites;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TBP.Core.CommandHandlers.LoginCommands
{
    public class Login
    {
        public class Command : ICommand<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string Token { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CommandHandler : ICommandHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly IConfiguration _configuration;

            public CommandHandler(IUserRepository userRepository, IConfiguration configuration)
            {
                _userRepository = userRepository;
                _configuration = configuration;
            }

            public async Task<Response> ExecuteAsync(Command command, CancellationToken ct)
            {
                try
                {
                    var user = await _userRepository.GetUserByUsernameAndPasswordAsync(command.Username, command.Password);
                    if (user == null)
                    {
                        return new Response { ErrorMessage = "Invalid username or password" };
                    }

                    var token = GenerateJwtToken(user);
                    return new Response { Token = token };
                }
                catch (Exception ex)
                {
                    return new Response
                    {
                        ErrorMessage = ex.Message
                    };
                }
            }

            private string GenerateJwtToken(User user)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName) // Add role claim

                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
