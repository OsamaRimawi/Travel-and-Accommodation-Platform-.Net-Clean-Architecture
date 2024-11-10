using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Get
{
    internal sealed class Request
    {
        public int Id { get; set; }
    }

    internal sealed class Response
    {
        public RoomDto Room { get; set; }
        public string ErrorMessage { get; set; }
    }
}