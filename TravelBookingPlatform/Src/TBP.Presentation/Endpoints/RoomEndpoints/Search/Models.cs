using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.RoomEndpoints.Search
{
    internal sealed class Request
    {
        [FromQuery]
        public decimal? Price { get; set; }
        [FromQuery]
        public int AdultCapacity { get; set; } = 2;
        [FromQuery]
        public int ChildCapacity { get; set; } = 0;
        [FromQuery]
        public bool Availability { get; set; } = true;
    }
    internal sealed class Response
    {
        public IEnumerable<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public string ErrorMessage { get; set; }
    }
}
