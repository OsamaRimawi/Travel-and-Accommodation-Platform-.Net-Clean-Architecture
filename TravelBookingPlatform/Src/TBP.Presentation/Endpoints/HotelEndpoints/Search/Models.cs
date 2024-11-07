using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TBP.Core.DTOs;

namespace TBP.Presentation.Endpoints.HotelEndpoints.Search
{
    internal sealed class Request
    {
        [FromQuery]
        public string? Name { get; set; }
        [FromQuery]
        public int? StarRating { get; set; }
        [FromQuery]
        public string? Location { get; set; }
        [FromQuery]
        public string? CityName { get; set; }
        [FromQuery]
        public string? Owner { get; set; }

    }
    internal sealed class Response
    {
        public IEnumerable<HotelDto> Hotels { get; set; } = new List<HotelDto>();
        public string ErrorMessage { get; set; }
    }
}
