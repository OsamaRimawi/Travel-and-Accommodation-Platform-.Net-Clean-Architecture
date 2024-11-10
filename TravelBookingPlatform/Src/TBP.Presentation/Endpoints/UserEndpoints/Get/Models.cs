using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.UserEndpoints.Get
{
    internal sealed class Request
    {
        public int Id { get; set; }
    }

    internal sealed class Response
    {
        public UserDto User { get; set; }
        public string ErrorMessage { get; set; }
    }
}