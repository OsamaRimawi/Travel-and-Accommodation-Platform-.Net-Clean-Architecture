using FastEndpoints;
using TBP.Core.DTOs;
using TBP.Domain.Entites;

namespace TBP.Presentation.Endpoints.RoomEndpoints.GetAll
{
    internal sealed class Response
    {
        public IEnumerable<RoomDto> Rooms { get; set; }
        public string ErrorMessage { get; set; }
    }
}